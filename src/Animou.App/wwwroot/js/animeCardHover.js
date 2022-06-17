function removeClassesFromAnimeCard() {
    $('.hover').removeClass('hover');
    $('.card-anime-title-hover').removeAttr('style');
    $('.card-anime-title-hover').removeClass('card-anime-title-hover');
};

function addHoverClassesToAnimeCard(element) {
    let randomColor = colors[Math.floor(Math.random() * colors.length)];
        element.children('.dropdown-anime-card').addClass('hover');

        // Change anime card color title
        element.children('.card-anime-title').addClass('card-anime-title-hover');
        $('.card-anime-title-hover').css('color', randomColor.primary);

        // Change anime card genre and studio color
        element.find('.card-anime-genre').css('background-color', randomColor.primary);
        element.find('.card-anime-genre').css('color', randomColor.secondary);
        element.find('.card-anime-genre').hover(function () {
            $(this).css('background-color', randomColor.hover);
        }, function () {
            $(this).css('background-color', randomColor.primary);
        });
        $('.card-anime-studio').css('color', randomColor.primary);

        // Change average score color
        var score = element.find('.card-anime-score');
        var thumb = element.find('.thumb');
        if (score.first().text() > 50) {
            thumb.removeClass('fa-solid fa-thumbs-down')
            thumb.addClass('fa-solid fa-thumbs-up')
        }
        else {
            thumb.removeClass('fa-solid fa-thumbs-up')
            thumb.addClass('fa-solid fa-thumbs-down')
        }
        if (score.first().text() < 21) $('.thumb').css('color', scoreColors[0].red)
        if (score.first().text() < 41 && score.first().text() > 20) $('.thumb').css('color', scoreColors[1].orange)
        if (score.first().text() < 61 && score.first().text() > 40) $('.thumn').css('color', scoreColors[2].yellow)
        if (score.first().text() < 81 && score.first().text() > 60) $('.thumb').css('color', scoreColors[3].lightGreen)
        if (score.first().text() > 80) $('.thumb').css('color', scoreColors[4].green)
}

$('.cards').on("mouseenter mouseleave mouseout mouseover", function (event) {

    if (window.matchMedia("(min-width: 1160px)").matches) {
        var target = event.target,
            lastTarget = event.relatedTarget,
            parent = $(target).parents('div').first();

        if (event.type == "mouseenter") {
            if (target.className == 'cards') return

            addHoverClassesToAnimeCard(parent);

        } else if ((event.type == "mouseleave")) {

            if (target.className != 'cards') removeClassesFromAnimeCard();

        } else if ((event.type == "mouseout")) {
            if (lastTarget == null) removeClassesFromAnimeCard()
            else if (lastTarget.className == 'cards') removeClassesFromAnimeCard();

        } else {
            if (lastTarget == null && event.type == "mouseover") addHoverClassesToAnimeCard(parent)
            else if (lastTarget.className == 'cards') addHoverClassesToAnimeCard(parent);
        }
    }
});
