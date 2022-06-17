var items = items;

if (typeof variables === 'undefined') {
    var variables = { perPage: 10 };
}

variables.page = 1;

if (!variables.hasOwnProperty('sort')) variables.sort = 'POPULARITY_DESC';

var animesId = [],
    animesStatus = [];

for (var i = 0; i < items.length; i++) {
    animesId.push(items[i].AnimeId);
    animesStatus.push(items[i].status);
}

variables.id_in = animesId;

function getAnimesList() {

    var query = `
        query ($page: Int, $perPage: Int, $status: MediaStatus, $search: String,
            $genre: String, $tag: String, $genre_in: [String], $tag_in: [String], 
            $sort: [MediaSort], $id_in: [Int]) {
            Page (page: $page, perPage: $perPage) {
                pageInfo {
                    hasNextPage
                }
                media (id_in: $id_in, type: ANIME, status: $status, search: $search,
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
            cardElement.classList.add('list-wrapper');
            document.querySelector('.section').appendChild(cardElement);

            var studios = "",
                genres = "",
                format = anime.format,
                episodes =
                    anime.episodes != null ? `${anime.episodes}` : "-",
                status = anime.status.substring(0, 1) +
                    anime.status.substring(1).toLowerCase(),
                listStatus = animesStatus[animesId.indexOf(anime.id)];

            switch (listStatus) {
                case 1: listStatus = 'Planning'
                    break;
                case 2: listStatus = 'Watching'
                    break;
                default: listStatus = 'Watched'
            }

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
                <div>
                    <a class="list-card">
                        <img class="list-anime-image" src="${anime.coverImage.large}">
                    </a>
                </div>
                <div class="list-anime-info" id=${anime.id}>
                    <div>
                        ${anime.title.romaji}
                    </div>
                    <div>
                        <div>
                            <span>Status:</span>
                        </div>
                        <div>
                            ${status}
                        </div>
                    </div>
                    <div>
                        <div>
                            <span>Episodes: </span>
                        </div>
                        <div>
                            ${episodes}
                        </div>
                    </div>
                    <div>
                        <div>
                            <span>List Status: </span>
                        </div>
                        <div>
                            <select class="list-select">
                                <option>${listStatus}</option>
                            </select>
                        </div>
                    </div>
                    <div>
                        <div class="remove">Remove</div>
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
getAnimesList();
