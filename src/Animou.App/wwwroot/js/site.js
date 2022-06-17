const colors = [
    { primary: '#f5cd54', secondary: '#997928', hover: '#ffff85' },
    { primary: '#5bf553', secondary: '#1e7d1e', hover: '#96ff85' },
    { primary: '#54def5', secondary: '#f5f5f6', hover: '#00acc2' },
    { primary: '#8d54f5', secondary: '#f5f5f6', hover: '#5725c1' },
    { primary: '#f5539b', secondary: '#f5f5f6', hover: '#be0f6d' }
]
const scoreColors = [
    { red: '#f35674' },
    { orange: '#f37f53' },
    { yellow: '#f3e45f' },
    { lightGreen: '#acf365' },
    { green: '#5bf368' },
]

if (typeof variables === 'undefined') variables = {};
var hasNextPage = true;

window.addEventListener('scroll', () => {
    const {
        scrollTop,
        scrollHeight,
        clientHeight
    } = document.documentElement;

    if (scrollTop + clientHeight >= scrollHeight &&
        hasNextPage) {
        setTimeout(async () => {
            variables.page++;
            searchAnimes();
        }, 500);
    }
});

$(document).ready(function () {
    $(".navbar-burger").click(function () {
        $(".navbar-burger").toggleClass("is-active");
        $(".navbar-menu").toggleClass("is-active");
    });

    $('.main-content').click(function (event) {
        if (window.location.pathname == "/anime") {
            if ($(event.target).hasClass('read-more-less')) {
                $('.details-synopsis').toggleClass('less');

                if ($('.details-synopsis').hasClass('less')) $(event.target).html('Read Less')
                else $(event.target).html('Read More')
            }
        }
    });

    //#region Add To List Dropdown
    if (window.location.pathname == "/anime") {
        var animeId = id;
    }

    let removeAlertTimeout;

    $(document).click(function (event) {
        var target = event.target;
        if (target.className == "add") {
            $.ajax({
                type: 'post',
                url: '/my-list/add',
                data: { status: 0, id: animeId },
                success: function (data) {
                    if (data == "NotLoggin") {
                        window.location.pathname = "Identity/Account/Register"
                    } else {
                        $('#listAlert').html(data)
                        removeAlertTimeout = setTimeout(() => { $('.alert').remove() }, 3000);

                        $('#addToList').removeClass('add')
                        $('#addToList').addClass('added')
                        $('#addToList').html('added')
                        $('#addToList').append('<i class="fa-solid fa-check"></i>')
                        $('.remove').css({ 'pointer-events': 'all', 'cursor': 'pointer' })
                    }
                }
            });

        } else if (target.className == "dropdown-set-as-wrapper" ||
            $(target).parents('.dropdown-set-as-wrapper').length) {
            $('.dropdown-set-as').toggleClass('open');

            if (target.className == "dropdown-set-as-item") {
                $.ajax({
                    type: 'POST',
                    url: '/my-list/add',
                    data: { status: $(target).val(), id: animeId },
                    success: function (data) {
                        $('#listAlert').html(data)
                        removeAlertTimeout = setTimeout(() => { $('.alert').remove() }, 3000);

                        if ($('#addToList').hasClass('add')) {
                            $('#addToList').removeClass('add')
                            $('#addToList').addClass('added')
                            $('#addToList').html('Added')
                            $('#addToList').apend('<i class="fa-solid fa-check"></i>')
                            $('.remove').css({ 'pointer-events': 'all', 'cursor': 'pointer' })
                        }
                    }
                });
            }

        } else if (target.className == "remove") {
            if (window.location.pathname == "/my-list") {
                var id = $(target).parents('.list-anime-info').attr('id');
                $.ajax({
                    type: 'POST',
                    url: '/my-list/remove',
                    data: { id: id, path: window.location.pathname },
                    success: function (data) {
                        $('#listAlert').html(data)
                        removeAlertTimeout = setTimeout(() => { $('.alert').remove() }, 3000);
                        location.reload();
                    }
                });
            } else {
                $.ajax({
                    type: 'POST',
                    url: '/my-list/remove',
                    data: { id: animeId, path: window.location.pathname },
                    success: function (data) {
                        $('#listAlert').html(data)
                        removeAlertTimeout = setTimeout(() => { $('.alert').remove() }, 3000);

                        $('#addToList').addClass('add')
                        $('#addToList').removeClass('added')
                        $('#addToList').empty()
                        $('#addToList').html('Add to List')
                        $('.remove').css({ 'pointer-events': 'none', 'cursor': 'deafult' })
                    }
                });
            }
        } else if (target.className == "delete") {
            clearTimeout(removeAlertTimeout);
            $('.alert').remove();
        } else {
            if ($('.dropdown-set-as').hasClass('open')) $('.dropdown-set-as').removeClass('open')
        }
    });
    //#endregion Add To List Dropdown

    //#region Anime Card Click
    $('.cards').click(function (event) {
        var target = event.target,
            parent = $(target).parents('div').first();
        console.log()
        if (target.className == 'cover-image' ||
            target.classList.contains('card-anime-title')) {
            $('#id').val(parent.attr('id'))
            $('#title').val(parent.find('.card-anime-title').text())
            $('#submitSelectedAnime').click()
        }
    });
    //#endregion Anime Card Click

    //#region Search Bar
    $('.input').hover(function () {
        $(this).css('color', "var(--color-quinary)")
        $(this).css('transition', 'color .2s ease')
        $(this).parent().find('.search-icon').css('color', 'var(--color-quinary)')
        $(this).parent().find('.search-icon').css('transition', 'color .2s ease')
    }, function () {
        $(this).css('color', "var(--color-tertiary)")
        $(this).parent().find('.search-icon').css('color', "var(--color-tertiary)")
    });
    $('.input').focus(function () {
        $(this).css('color', "var(--color-quinary)")
        $(this).parent().find('.search-icon').css('color', "var(--color-quinary)")
    });
    //#endregion Search Bar

    //#region Dropdown Filter Open/Close

    $(".button-filter").click(function () {
        $(this).parent().find('.dropdown-filter').toggleClass("focus");
    });

    $(document).mouseup(e => {
        if (!$(".dropdown-filter").is(e.target) && $(".dropdown-filter").has(e.target).length === 0) {
            $(".dropdown-filter").removeClass('focus');
        }
    });

    $('.dropdown-header').find('i').click(function () {
        $('.dropdown-filter').removeClass('focus');
    });

    $(window).click(e => {
        if ($(".dropdown-filter").is(e.target)) {
            $(".dropdown-filter").removeClass('focus');
        }
    });

    //#endregion Dropdown Filter Open/Close

    //#region Dropdown Filter Items
    $('.dropdown-items').click(function () {
        var value = $(this).first().text().trim();
        $(this).toggleClass('item-selected');

        if ($(this).hasClass('item-selected')) {
            $('.genres-and-tags').append(`<button class="${value}">${value}</button>`);
            AddFunctionToSearch(addFiltersToUrl, value)

            $('#filter').val($(this).first().text().trim());
            $('#filter').attr('name', 'filter');

            addGenreToVariables(value);
            variables.page = 1;
            console.log(variables)
        }
        else {
            $('.genres-and-tags').find(`.${value}`).remove();
            AddFunctionToSearch(removeFiltersFromUrl, value);

            //$(this).parent().find('#filter').removeAttr('value');
            if (!checkIfElementsHaveClass('.dropdown-items', 'item-selected'))
                $('#filter').removeAttr('name');

            removeGenreFromVariables(value);
            variables.page = 1;
        }
    });

    $('.genres-and-tags').on('click', '> *', function () {
        var value = $(this).first().text().replace(' ', '-');
        $('.dropdown-items-wrapper').find(`.${value}`).click();
        $('.genres-and-tags').find(this).remove();
        AddFunctionToSearch(removeFiltersFromUrl, value);
    });
    //#endregion Dropdown Filter Items

    //#region Search Submit

    $('#submit').click(function (event) {
        if ($('#search').val() != "") {
            var value = $('#search').val();
            $('#search').attr('name', 'search');
            AddFunctionToSearch(addSearchToUrl, value);
            addSearchToVariables(value);
            variables.page = 1;

        } else {
            $('#search').removeAttr('name');
            AddFunctionToSearch(removeSearchFromUrl, null);
            removeSearchFromVariables();
            variables.page = 1;
        }
    });

    //#endregion Search Submit

    //#region Search Page
    AddFunctionToSearch(addFiltersAndValuesToSearchPage, null)

    //#endregion Search Page

    function arrayRemove(arr, value) {
        return arr.filter(function (element) {
            return element != value;
        });
    };

    function addFiltersAndValuesToSearchPage(path) {
        var parameters = new URLSearchParams(path);
        var filters = parameters.getAll('filter');
        var search = parameters.get('search');

        $('.dropdown-items').each(function () {
            var elementContent = this.textContent.trim();
            if (filters != null) {
                filters.forEach(genre => {
                    if (genre == elementContent) {
                        this.classList.add('item-selected');
                        $('.genres-and-tags').append(
                            `<button class="${elementContent}">${elementContent}</button>`);
                    };
                });
            };
        });

        $('#search').val(search);
    }

    function addFiltersToUrl(path, value) {
        var parameters = new URLSearchParams(path);
        var pathName = window.location.pathname + "?";

        if (!parameters.has('search')) {
            parameters.append('filter', `${value}`);
        } else {
            var search = parameters.get('search');
            parameters.delete('search');
            parameters.append('filter', value);
            parameters.append('search', search);
        }

        window.history.pushState('', '', pathName + parameters.toString());
        urlParameters = parameters.toString();
    }

    function AddFunctionToSearch(callBack, value) {
        if (!window.location.pathname.includes("search")) return;

        var getUrlParameters = urlParameters;
        callBack(getUrlParameters, value);
    }

    function addSearchToUrl(path, value) {
        var parameters = new URLSearchParams(path);
        var pathName = window.location.pathname + "?";

        if (!parameters.has('search')) {
            parameters.append('search', value);
        } else {
            parameters.set('search', value);
        }

        window.history.pushState('', '', pathName + parameters.toString());
        urlParameters = parameters.toString();
    }

    function checkIfElementsHaveClass(elements, elementsClass) {
        var hasClass = false;
        $(elements).each(function () {
            if ($(this).hasClass(elementsClass)) { hasClass = true }
        });

        return hasClass;
    }

    function removeFiltersFromUrl(path, value) {
        var pathName = window.location.pathname;
        var parameters = new URLSearchParams(path);
        var filters = parameters.getAll('filter');
        var search = parameters.get('search');

        parameters.delete('filter');

        filters.forEach(function (element) {
            if (element == value) {
                filters = arrayRemove(filters, value)
            } else {
                parameters.append('filter', element);
            }
        });

        if (search != null) {
            parameters.delete('search');
            parameters.append('search', search);
        };

        pathName += parameters.toString() == "" ? "" : "?"
        window.history.pushState('', '', pathName + parameters.toString());
        urlParameters = parameters.toString();

    }

    function removeSearchFromUrl(path) {
        var pathName = window.location.pathname;
        var parameters = new URLSearchParams(path);
        parameters.delete('search');
        pathName += parameters.toString() == "" ? "" : "?";
        window.history.pushState('', '', pathName + parameters.toString());
        urlParameters = parameters.toString();
    }


    function addGenreToVariables(newGenre) {
        if (variables.hasOwnProperty('genre')) {
            var genres = [variables.genre, newGenre];
            delete variables.genre;
            variables.genre_in = genres;
        }
        else if (variables.hasOwnProperty('genre_in')) {
            variables.genre_in.push(newGenre);
        } else {
            variables.genre = newGenre;
        }
    }

    function removeGenreFromVariables(genre) {
        if (variables.hasOwnProperty('genre')) {
            delete variables.genre;
        } else {
            variables.genre_in.forEach(key => {
                if (key == genre) {
                    var index = variables.genre_in.indexOf(genre);
                    variables.genre_in.splice(index, 1);
                }
            });
            if (variables.genre_in.length < 2) {
                variables.genre = variables.genre_in[0];
                delete variables.genre_in;
            }
        }
    }

    function addSearchToVariables(search) {
        variables.search = search;
    }

    function removeSearchFromVariables() {
        delete variables.search;
    }

});
