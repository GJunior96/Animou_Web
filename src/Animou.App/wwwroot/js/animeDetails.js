if (typeof variables === 'undefined') {
    var variables = { id: id };
} else {
    variables = {};
    variables = { id: id };
}

var list = onList;

function getAnimeDetails() {
    var query = `
        query ($id: Int) {
            Media (id: $id) {
                id title { romaji english } averageScore description seasonYear coverImage { large }
                format episodes genres status studios { edges { node { name } } } 
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

    function handleData(dataResult) {

        const section = document.createElement('section'),
              anime = dataResult.data.Media;

        var actionButton = list == 'False' ? '<div id="addToList" class="add">Add to list</div>' :
                        '<div id="addToList" class="added">Added<i class="fa-solid fa-check"></i></div>'
            removeButton = list == 'False' ? '<div class="remove disabled">Remove</div>' :
                        '<div class="remove">Remove</div>'

        section.classList.add('section');
        document.querySelector('.main-content').appendChild(section);

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
            studios += `<span class="details-studios">${value.node.name},</span>`
        }

        for (const [index, value] of anime.genres.entries()) {
            genres += `<a class="datails-genres">${value}</a>`
        }

        section.innerHTML = `
            <div class="details">
                <div class="details-card">
                    <div> <img class="details-cover-image" src="${anime.coverImage.large}" /> </div>
                    <div class="actions">
                        <div class="add-buttons">
                            ${actionButton}
                            <div class="dropdown-set-as-wrapper">
                                <div class="dropdown-arrow">
                                    <span><i class="fa-solid fa-caret-down"></i></span>
                                </div>
                                <ul class="dropdown-set-as" id="dropdownSetAs">
                                    <li value=2 class="dropdown-set-as-item">Set as Watching</li>
                                    <li value=1 class="dropdown-set-as-item">Set as Planning</li>
                                    <li value=3 class="dropdown-set-as-item">Set as Watched</li>
                                </ul>
                            </div>
                        </div>
                        ${removeButton}
                    </div>
                </div>

                <div class="details-info synopsis">
                    <div class="details-synopsis">
                        <h3>${anime.title.romaji}<h3>
                        <h4>${anime.title.english}<h4>
                        <p>${anime.description}<p>
                    </div>
                    <button class="read-more-less">Read More</button>
                </div>

                <div class="details-info extra">
                    <div class="details-wrapper">
                        <span>Status:</span>
                        <div><span>${status}</span></div>
                    </div>
                    <div class="details-wrapper">
                        <span>Year:</span>
                        <div><span>${anime.seasonYear}</span></div>
                    </div>
                    <div class="details-wrapper">
                        <span>Format:</span>
                        <div><span>${format}</span></div>
                    </div>
                    <div class="details-wrapper">
                        <span>Episodes:</span>
                        <div><span>${anime.episodes}</span></div>
                    </div>
                    <div class="details-wrapper">
                        <span>Genres:</span>
                        <div>${genres}</div>
                    </div>
                    <div class="details-wrapper">
                        <span>Studios:</span>
                        <div>${studios}</div>
                    </div>
                </div>
            </div>
            `;
    }

    function handleError(error) {
        alert('Error, check console');
        console.error(error);
    }
};
getAnimeDetails();