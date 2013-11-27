(function ($) {
    function setSlideShow(el, options) {
        if ($(el).data("slideshow") == null || undefined) {
            var ss = new slideShow(el, options);
            $(el).data("slideshow", ss);
        }
    }

    function slideShow(el, options) {
        var target = $(el);
        var id = $(target).attr("id");

        var config =
            {
                itemtag: "div",
                viewableCount: 1,
                width: 500,
                height: 400,
                loop: true,
                beforeSlide: function (slideNum) { },
                afterSlide: function (slideNum) { },
                nextButton: null,
                previousButton: null
            }

        $.extend(config, options);
        config.itemWidth = config.width / config.viewableCount;

        var itemContainer = document.createElement("div");
        itemContainer.setAttribute("id", id + "_itemcontainer");
        var itemContainerWidth = 0;

        $(itemContainer).css("position", "absolute");
        $(target).width(config.width).height(config.height).css("overflow", "hidden").css("position", "relative");

        var slides = [];
        $(target).children().each(function (i, el) {
            $(el).css("float", "left").width(config.itemWidth).height(config.height);
            itemContainerWidth += $(el).outerWidth(true); //true -> include margin
            el.parentNode.removeChild(el);
            itemContainer.appendChild(el);
            slides.push(el);
        });

        this.element = target;
        this.slides = slides;
        this.itemContainer = itemContainer;
        this.currentSlideIndex = 0;
        this.loop = config.loop;

        $(itemContainer).width(itemContainerWidth);

        $(target).append(itemContainer);

        var the = this;

        var getSlideLeft = function (slideIndex) {
            if (slideIndex < 0) {
                throw { message: "SlideIndex out of range" }
            }
            if (slideIndex > the.slides.length - 1) {
                throw { message: "SlideIndex out of range" };
            }
            var left = 0;
            for (var i = 0; i < slideIndex; i++) {
                left += $(the.slides[i]).outerWidth(true);
            }

            return -left;
        }
        this.getSlideLeft = getSlideLeft;

        var getCurrentSlide = function () {
            return the.slides[the.currentSlideIndex];
        }

        this.getCurrentSlide = getCurrentSlide;

        this.getCurrentSlideIndex = function () {
            return the.currentSlideIndex;
        }

        this.getSlideCount = function () {
            return the.slides.length;
        }

        var setSlide = function (slideIndex) {
            // the first slide is a copy of the last for 'looping' effect
            if (config.loop == true && slideIndex == 0) {
                slideIndex = the.slides.length - (config.viewableCount * 2);
            }

            // the last slide is a copy of the first for 'looping' effect
            if (config.loop == true && slideIndex == the.slides.length - (config.viewableCount)) {
                slideIndex = config.viewableCount;
            }
            the.currentSlideIndex = slideIndex;
            $(the.itemContainer).css("left", the.getSlideLeft(slideIndex))
        }

        var firstPosition = function () {
            the.currentSlideIndex = 0;
            if (config.loop == true) {
                the.currentSlideIndex = config.viewableCount;
            }
            var left = getSlideLeft(the.currentSlideIndex);
            $(the.itemContainer).css("left", left);
        }

        this.firstPosition = firstPosition;

        var getMoveBy = function (from, to) {
            if (from < 0 || from > the.slides.length - 1) {
                throw { message: "from index out of range" };
            }

            if (to < 0 || to > the.slides.length - 1) {
                throw { message: "to index out of range" };
            }

            var low = from > to ? to : from;
            var high = from > to ? from : to;
            var dir = to > from ? "-=" : "+=";
            // mainly for debugging, a 'friendly' way of determining the direction of the slide
            var fr = dir == "-=" ? "up" : "down"; // yes minus (-) is "up"

            var moveByWidth = 0;
            for (var i = low; i < high; i++) {
                moveByWidth += $(the.slides[i]).outerWidth();
            }

            return { width: moveByWidth, direction: dir, friendly: fr };
        }

        this.getMoveBy = getMoveBy;

        var slideTo = function (slideIndex) {
            if (slideIndex > the.slides.length - 1) {
                slideIndex = the.slides.length - 1;
            } else if (slideIndex < 0) {
                slideIndex = 0;
            }

            config.beforeSlide(slideIndex);
            var moveBy = getMoveBy(the.currentSlideIndex, slideIndex);
            $(the.itemContainer).animate({ left: moveBy.direction + moveBy.width + "px" }, "swing", function () {
                setSlide(slideIndex);
                config.afterSlide(slideIndex);
            });
        }

        if (config.loop == true) {
            var frontClones = [];
            var backClones = [];
            for (var i = 0; i < config.viewableCount; i++) {
                var items = $(itemContainer).children();
                var frontClone = $(items[i]).clone();
                frontClones.push(frontClone);
                var backClone = $(items[items.length - (i + 1)]).clone();
                backClones.push(backClone);
            }
            for (var i = 0; i < backClones.length; i++) {
                var clone = backClones[i];
                $(itemContainer).children().first().before(clone);
                itemContainerWidth += $(clone).outerWidth();
                this.slides.splice(0, 0, clone);
            }
            for (var i = 0; i < frontClones.length; i++) {
                var clone = frontClones[i];
                $(itemContainer).children().last().after(clone);
                itemContainerWidth += $(clone).outerWidth();
                this.slides.push(clone);
            }
            // -- old impl, supports viewable count 1 only
            //            var firstClone = $(itemContainer).children().first().clone();
            //            var lastClone = $(itemContainer).children().last().clone();
            //            itemContainerWidth += $(firstClone).outerWidth();
            //            itemContainerWidth += $(lastClone).outerWidth();
            //            $(itemContainer).children().first().before(lastClone);
            //            $(itemContainer).children().last().after(firstClone);
            //            this.slides.splice(0, 0, lastClone);
            //            this.slides.push(firstClone);            
            // --

            $(this.itemContainer).width(itemContainerWidth);
        }

        this.slideTo = slideTo;

        this.nextSlide = function () {
            var next = the.currentSlideIndex + 1;
            slideTo(next);
        }

        this.previousSlide = function () {
            var prev = the.currentSlideIndex - 1;
            slideTo(prev);
        }

        this.firstPosition();
        $(config.nextButton).click(this.nextSlide);
        $(config.previousButton).click(this.previousSlide);
    }

    $.fn.slideshow = function (options) {
        var config =
        {
            itemtag: "div",
            width: 500,
            height: 400,
            loop: true,
            beforeSlide: function (slideNum) { },
            afterSlide: function (slideNum) { },
            nextButton: null,
            previousButton: null
        }

        return this.each(function () {
            if (options) {
                $.extend(config, options);
            }

            var the = $(this);
            setSlideShow(the, options);
        });
    }
})(jQuery);
