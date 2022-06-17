if (typeof variables === 'undefined') {
    var variables = { perPage: 15 }
}

variables.page = 1;

if (!variables.hasOwnProperty('sort')) variables.sort = 'POPULARITY_DESC';

function searchAnimes() {
    const cardsElement = document.querySelector('.cards');

    if (!variables.hasOwnProperty('search') &&
        !variables.hasOwnProperty('genre') && !variables.hasOwnProperty('genre_in') &&
        !variables.hasOwnProperty('tag') && !variables.hasOwnProperty('tag_in')) {

        var parameters = new URLSearchParams(urlParameters.value),
            filters = parameters.getAll('filter'),
            search = parameters.get('search');

        if (search != null) variables.search = search;

        if (filters.length > 1) variables.genre_in = filters;
        else variables.genre = filters[0];
    }


    var query = `
        query ($page: Int, $perPage: Int, $status: MediaStatus, $search: String,
            $genre: String, $tag: String, $genre_in: [String], $tag_in: [String], 
            $sort: [MediaSort]) {
            Page (page: $page, perPage: $perPage) {
                pageInfo {
                    hasNextPage
                }
                media (search: $search, type: ANIME, status: $status, 
                    sort: $sort, genre_in: $genre_in, tag_in: $tag_in,
                    genre: $genre, tag: $tag) {
                    id title { romaji english } averageScore seasonYear coverImage { large }
                    format episodes genres status studios { edges { node { name } } } 
                }
            }
        }
        `;
    const url = 'https://graphql.anilist.co',
        options = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json',
            },
            body: JSON.stringify({
                query: query,
                variables: variables
            })
        }
    fetch(url, options).then(handleResponse)
        .then(handleData)
        .catch(handleError);

    function handleResponse(response) {
        return response.json().then(function (json) {
            return response.ok ? json : Promise.reject(json);
        });
    }

    function handleData(cards) {
        cards.data.Page.media.forEach(anime => {
            const cardElement = document.createElement('div');
            cardElement.classList.add('card-anime');
            cardElement.setAttribute('id', anime.id)
            document.querySelector('.cards').appendChild(cardElement);

            var studios = "",
                genres = "",
                format = anime.format,
                status = anime.status.substring(0, 1) +
                         anime.status.substring(1).toLowerCase();

            switch (format) {
                case "TV": format = "TV Show"
                    break;
                case "TV_SHORT": format = "TV Short"
                    break;
            }

            for (const [index, value] of anime.studios.edges.entries()) {
                if (index > 1) break;
                studios += `<span class="card-anime-studio">${value.node.name}</span>`
            }

            for (const [index, value] of anime.genres.entries()) {
                if (index > 3) break;
                genres += `<span class="card-anime-genre">${value}</span>`
            }

            cardElement.innerHTML = `
                <a class="card-cover">
                    <img class="cover-image" src="${anime.coverImage.large}">
                </a>
                <a class="card-anime-title">${anime.title.romaji}</a>
                <div class="dropdown-anime-card">
                    <div class="card-anime-status-wrapper">
                        <span class="card-anime-status">${status}</span>
                        <span class="card-anime-score">
                            <i class="thumb"></i>${anime.averageScore}
                        </span>
                    </div>
                    <div>
                        ${studios}
                    </div>
                    <div>
                        <span class="card-anime-info">${format}</span>
                        <span class="card-anime-info">${anime.episodes}</span>
                    </div>
                    <div>
                        ${genres}
                    </div>
                </div>
                `;
        })
        hasNextPage = cards.data.Page.pageInfo.hasNextPage;
    }

    function handleError(error) {
        alert('Error, check console');
        console.error(error);
    }
};
searchAnimes();


