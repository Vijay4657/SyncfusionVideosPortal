var mr = (function ($, window, document) {
    "use strict";

    var mr = {},
        components = { documentReady: [], documentReadyDeferred: [], windowLoad: [], windowLoadDeferred: [] };


    $(document).ready(documentReady);
    $(window).on("load", windowLoad);

    function documentReady(context) {

        context = typeof context === typeof undefined ? $ : context;
        components.documentReady.concat(components.documentReadyDeferred).forEach(function (component) {
            component(context);
        });
    }

    function windowLoad(context) {

        context = typeof context === "object" ? $ : context;
        components.windowLoad.concat(components.windowLoadDeferred).forEach(function (component) {
            component(context);
        });
    }

    mr.setContext = function (contextSelector) {
        var context = $;
        if (typeof contextSelector !== typeof undefined) {
            return function (selector) {
                return $(contextSelector).find(selector);
            };
        }
        return context;
    };

    mr.components = components;
    mr.documentReady = documentReady;
    mr.windowLoad = windowLoad;

    return mr;
}(jQuery, window, document));

//////////////// Backgrounds
mr = (function (mr, $, window, document) {
    "use strict";

    var documentReady = function ($) {

        //////////////// Append .background-image-holder <img>'s as CSS backgrounds

        $('.background-image-holder').each(function () {
            var imgSrc = $(this).children('img').attr('src');
            $(this).css('background', 'url("' + imgSrc + '")').css('background-position', 'initial').css('opacity', '1');
        });
    };

    mr.backgrounds = {
        documentReady: documentReady
    };

    mr.components.documentReady.push(documentReady);
    return mr;

}(mr, jQuery, window, document));

//////////////// Scroll Functions
mr = (function (mr, $, window, document) {
    "use strict";


    mr.scroll = {};
    var raf = window.requestAnimationFrame ||
                          window.mozRequestAnimationFrame ||
                          window.webkitRequestAnimationFrame ||
                          window.msRequestAnimationFrame;
    mr.scroll.listeners = [];
    mr.scroll.busy = false;
    mr.scroll.y = 0;
    mr.scroll.x = 0;

    var documentReady = function ($) {

        //////////////// Capture Scroll Event and fire scroll function
        jQuery(window).off('scroll.mr');
        jQuery(window).on('scroll.mr', function (evt) {
            if (mr.scroll.busy === false) {

                mr.scroll.busy = true;
                raf(function (evt) {
                    mr.scroll.update(evt);
                });

            }
            if (evt.stopPropagation) {
                evt.stopPropagation();
            }
        });

    };

    mr.scroll.update = function (event) {

        // Loop through all mr scroll listeners
        var parallax = typeof window.mr_parallax !== typeof undefined ? true : false;
        mr.scroll.y = (parallax ? mr_parallax.mr_getScrollPosition() : window.pageYOffset);
        mr.scroll.busy = false;
        if (parallax) {
            mr_parallax.mr_parallaxBackground();
        }


        if (mr.scroll.listeners.length > 0) {
            for (var i = 0, l = mr.scroll.listeners.length; i < l; i++) {
                mr.scroll.listeners[i](event);
            }
        }

    };

    mr.scroll.documentReady = documentReady;

    mr.components.documentReady.push(documentReady);

    return mr;

}(mr, jQuery, window, document));

//////////////// Utility Functions
mr = (function (mr, $, window, document) {
    "use strict";
    mr.util = {};

    mr.util.requestAnimationFrame = window.requestAnimationFrame ||
                                       window.mozRequestAnimationFrame ||
                                       window.webkitRequestAnimationFrame ||
                                       window.msRequestAnimationFrame;

    mr.util.documentReady = function ($) {
        var today = new Date();
        var year = today.getFullYear();
        $('.update-year').text(year);
    };

    mr.util.getURLParameter = function (name) {
        return decodeURIComponent((new RegExp('[?|&]' + name + '=' + '([^&;]+?)(&|#|;|$)').exec(location.search) || [undefined, ""])[1].replace(/\+/g, '%20')) || null;
    };


    mr.util.capitaliseFirstLetter = function (string) {
        return string.charAt(0).toUpperCase() + string.slice(1);
    };

    mr.util.slugify = function (text, spacesOnly) {
        if (typeof spacesOnly !== typeof undefined) {
            return text.replace(/ +/g, '');
        } else {
            return text
                .toLowerCase()
                .replace(/[\~\!\@\#\$\%\^\&\*\(\)\-\_\=\+\]\[\}\{\'\"\;\\\:\?\/\>\<\.\,]+/g, '')
                .replace(/ +/g, '-');
        }
    };

    mr.util.sortChildrenByText = function (parentElement, reverse) {
        var $parentElement = $(parentElement);
        var items = $parentElement.children().get();
        var order = -1;
        var order2 = 1;
        if (typeof reverse !== typeof undefined) { order = 1; order2 = -1; }

        items.sort(function (a, b) {
            var keyA = $(a).text();
            var keyB = $(b).text();

            if (keyA < keyB) return order;
            if (keyA > keyB) return order2;
            return 0;
        });

        // Append back into place
        $parentElement.empty();
        $(items).each(function (i, itm) {
            $parentElement.append(itm);
        });
    };

    // Set data-src attribute of element from src to be restored later
    mr.util.idleSrc = function (context, selector) {

        selector = (typeof selector !== typeof undefined) ? selector : '';
        var elems = context.is(selector + '[src]') ? context : context.find(selector + '[src]');

        elems.each(function (index, elem) {
            elem = $(elem);
            var currentSrc = elem.attr('src'),
                dataSrc = elem.attr('data-src');

            // If there is no data-src, save current source to it
            if (typeof dataSrc === typeof undefined) {
                elem.attr('data-src', currentSrc);
            }

            // Clear the src attribute
            elem.attr('src', '');

        });
    };

    // Set src attribute of element from its data-src where it was temporarily stored earlier
    mr.util.activateIdleSrc = function (context, selector) {

        selector = (typeof selector !== typeof undefined) ? selector : '';
        var elems = context.is(selector + '[src]') ? context : context.find(selector + '[src]');

        elems.each(function (index, elem) {
            elem = $(elem);
            var dataSrc = elem.attr('data-src');

            // If there is no data-src, save current source to it
            if (typeof dataSrc !== typeof undefined) {
                elem.attr('src', dataSrc);
            }
        });
    };

    mr.util.pauseVideo = function (context) {
        var elems = context.is('video') ? context : context.find('video');

        elems.each(function (index, video) {
            var playingVideo = $(video).get(0);
            playingVideo.pause();
        });
    };

    // Take a text value in either px (eg. 150px) or vh (eg. 65vh) and return a number in pixels.
    mr.util.parsePixels = function (text) {
        var windowHeight = $(window).height(), value;

        // Text text against regular expression for px value.
        if (/^[1-9]{1}[0-9]*[p][x]$/.test(text)) {
            return parseInt(text.replace('px', ''), 10);
        }
            // Otherwise it is vh value.
        else if (/^[1-9]{1}[0-9]*[v][h]$/.test(text)) {
            value = parseInt(text.replace('vh', ''), 10);
            // Return conversion to percentage of window height.
            return windowHeight * (value / 100);
        } else {
            // If it is not proper text, return -1 to indicate bad value.
            return -1;
        }
    };

    mr.components.documentReady.push(mr.util.documentReady);
    return mr;

}(mr, jQuery, window, document));
//////////////// Scroll Class Modifier
mr = (function (mr, $, window, document) {
    "use strict";

    mr.scroll.classModifiers = {};
    // Globally accessible list of elements/rules
    mr.scroll.classModifiers.rules = [];

    mr.scroll.classModifiers.parseScrollRules = function (element) {
        var text = element.attr('data-scroll-class'),
            rules = text.split(";");

        rules.forEach(function (rule) {
            var ruleComponents, scrollPoint, ruleObject = {};
            ruleComponents = rule.replace(/\s/g, "").split(':');
            if (ruleComponents.length === 2) {
                scrollPoint = mr.util.parsePixels(ruleComponents[0]);
                if (scrollPoint > -1) {
                    ruleObject.scrollPoint = scrollPoint;
                    if (ruleComponents[1].length) {
                        var toggleClass = ruleComponents[1];
                        ruleObject.toggleClass = toggleClass;
                        // Set variable in object to indicate that element already has class applied
                        ruleObject.hasClass = element.hasClass(toggleClass);
                        ruleObject.element = element.get(0);
                        mr.scroll.classModifiers.rules.push(ruleObject);
                    } else {
                        // Error: toggleClass component does not exist.
                        //console.log('Error - toggle class not found.');
                        return false;
                    }
                } else {
                    // Error: scrollpoint component was malformed
                    //console.log('Error - Scrollpoint not found.');
                    return false;
                }
            }
        });

        if (mr.scroll.classModifiers.rules.length) {
            return true;
        } else {
            return false;
        }
    };

    mr.scroll.classModifiers.update = function (event) {
        var currentScroll = mr.scroll.y,
            scrollRules = mr.scroll.classModifiers.rules,
            l = scrollRules.length,
            currentRule;

        // Given the current scrollPoint, check for necessary changes 
        while (l--) {

            currentRule = scrollRules[l];

            if (currentScroll > currentRule.scrollPoint && !currentRule.hasClass) {
                // Set local copy and glogal copy at the same time;
                currentRule.element.classList.add(currentRule.toggleClass);
                currentRule.hasClass = mr.scroll.classModifiers.rules[l].hasClass = true;
            }
            if (currentScroll < currentRule.scrollPoint && currentRule.hasClass) {
                // Set local copy and glogal copy at the same time;
                currentRule.element.classList.remove(currentRule.toggleClass);
                currentRule.hasClass = mr.scroll.classModifiers.rules[l].hasClass = false;
            }
        }
    };

    var fixedElementSizes = function () {
        $('.main-container [data-scroll-class*="pos-fixed"]').each(function () {
            var element = $(this);
            element.css('max-width', element.parent().outerWidth());
            element.parent().css('min-height', element.outerHeight());
        });
    };

    var documentReady = function ($) {
        // Collect info on all elements that require class modification at load time
        // Each element has data-scroll-class with a formatted value to represent class to add/remove at a particular scroll point.
        $('[data-scroll-class]').each(function () {
            var element = $(this);

            // Test the rules to be added to an array of rules.
            if (!mr.scroll.classModifiers.parseScrollRules(element)) {
                console.log('Error parsing scroll rules on: ' + element);
            }
        });

        // For 'position fixed' elements, give them a max-width for correct fixing behaviour
        fixedElementSizes();
        $(window).on('resize', fixedElementSizes);

        // If there are valid scroll rules add classModifiers update function to the scroll event processing queue.
        if (mr.scroll.classModifiers.rules.length) {
            mr.scroll.listeners.push(mr.scroll.classModifiers.update);
        }
    };

    mr.components.documentReady.push(documentReady);
    mr.scroll.classModifiers.documentReady = documentReady;



    return mr;

}(mr, jQuery, window, document));

//////////////// Smoothscroll
mr = (function (mr, $, window, document) {
    "use strict";

    mr.smoothscroll = {};
    mr.smoothscroll.sections = [];

    mr.smoothscroll.init = function () {
        mr.smoothscroll.sections = [];

        $('a.inner-link').each(function () {
            var sectionObject = {},
                link = $(this),
                href = link.attr('href'),
                validLink = new RegExp('^#[^\n^\s^\#^\.]+$');

            if (validLink.test(href)) {

                if ($('section' + href).length) {
                    sectionObject.id = href;
                    sectionObject.top = Math.round($(href).offset().top);
                    sectionObject.height = Math.round($(href).outerHeight());
                    sectionObject.link = link.get(0);
                    sectionObject.active = false;

                    mr.smoothscroll.sections.push(sectionObject);
                }
            }
        });

        mr.smoothscroll.highlight();
    };

    mr.smoothscroll.highlight = function () {
        mr.smoothscroll.sections.forEach(function (section) {
            if (mr.scroll.y >= section.top && mr.scroll.y < (section.top + section.height)) {
                if (section.active === false) {
                    section.link.classList.add("inner-link--active");
                    section.active = true;
                }
            } else {
                section.link.classList.remove("inner-link--active");
                section.active = false;
            }
        });
    };

    mr.scroll.listeners.push(mr.smoothscroll.highlight);

    var documentReady = function ($) {
        // Smooth scroll to inner links
        var innerLinks = $('a.inner-link');

        if (innerLinks.length) {
            innerLinks.each(function (index) {
                var link = $(this),
                    href = link.attr('href');
                if (href.charAt(0) !== "#") {
                    link.removeClass('inner-link');
                }
            });

            mr.smoothscroll.init();
            $(window).on('resize', mr.smoothscroll.init);

            var offset = 0;
            if ($('body[data-smooth-scroll-offset]').length) {
                offset = $('body').attr('data-smooth-scroll-offset');
                offset = offset * 1;
            }

            smoothScroll.init({
                selector: '.inner-link',
                selectorHeader: null,
                speed: 750,
                easing: 'easeInOutCubic',
                offset: offset
            });
        }
    };

    mr.smoothscroll.documentReady = documentReady;

    mr.components.documentReady.push(documentReady);
    mr.components.windowLoad.push(mr.smoothscroll.init);
    return mr;

}(mr, jQuery, window, document));




var industryFilters;

var searchRegex;
var searchTerm;
$(document).ready(function () {
    $(".industry_select_picker").multiselect();
    // to remove the first "choose industry message" in select dropdown
    $(".multiselect-container").find("li").first().remove();

    $(".industry_select").removeClass("hide");
    $(".technology_select").removeClass("hide");
    // to select url industry value 
    var selectedIndustry = window.location.pathname.split('/')[2];
    $('.industry_select_picker').multiselect('select', selectedIndustry);

    industryFilters = $(".industry_select_picker").val();

    $('.case-studies-section').isotope({
        itemSelector: '.case-studies-segments',
        layoutMode: 'fitRows',
        filter: function () {
            var $this = $(this);
            industryFilters = $(".industry_select_picker").val() != "all" ? ("." + $(".industry_select_picker").val()) : "*";
            var searchResult = searchRegex ? $this.text().match(searchRegex) : true;
            var industryResult = industryFilters ? $this.is(industryFilters) : true;
            return searchResult && industryResult;

        }
    });


    var queries = getQueryString();
    if (queries != "" && queries != null && queries["searchkey"] != undefined) {
        $('#search-box').val(queries["searchkey"].split(","));
    }

    if ($("#search-box").val().trim() != "") {

        if ($("#clear-filter").css("display") == "none") {
            $("#clear-filter").fadeIn();
            $("#filter-search-icon").fadeOut();
        }

    } else {
        if ($("#clear-filter").css("display") != "none") {
            $("#clear-filter").fadeOut();
            $("#filter-search-icon").fadeIn();
        }
    }

    if (selectedIndustry != "" && selectedIndustry != null && selectedIndustry != undefined || queries != "" && queries != null && queries["searchkey"] != undefined) {
        RefreshCaseStudies();
    }
    $(".masonry-filter-holder").first().css("z-index", 2);
    $(".card__top").matchHeight();
    $(".card__body").matchHeight();
    
});
function getQueryString() {
    var queries = {};
    if (document.location.search != "") {
        $.each(document.location.search.substr(1).split('&'), function (c, q) {
            var i = q.split('=');
            queries[i[0].toString()] = i[1].toString();
        });
    }
    return queries;
}
function RefreshCaseStudies() {

    searchRegex = new RegExp($('#search-box').val(), 'gi');

    $(".case-studies-section").isotope();

    setTimeout(function () {
        if ($('#search-box').val() != "") {

            $(".results-section h2 span:first-child").text('"' + $('#search-box').val() + '"');

            // to set no of result 
            if ($(".case-studies-section .masonry__item:visible").length == 0) {
                $(".results-section p span:first-child").text("No");
            } else {
                $(".results-section p span:first-child").text($(".case-studies-section .masonry__item:visible").length);
            }

            // to set industry section.
            if ($(".industry_select_picker").val() == "all") {
                $(".results-section h2 #industry-text").text("All");
                $(".results-section h2 span:last-child").text("Industries");

            } else {
                $(".results-section h2 #industry-text").text('"' + $(".industry_select_picker [value=" + $(".industry_select_picker").val() + "]").text() + '"');
                $(".results-section h2 span:last-child").text("Industry");
            }

            $(".results-section").slideDown("slow");
        } else {
            $(".results-section").slideUp("slow");
        }

    }, 500);
}
$(document).on("click", "#clear-filter", function (e) {
    $("#clear-filter").fadeOut("slow");
    $("#filter-search-icon").fadeIn();
    $('#search-box').val("");
    RefreshCaseStudies();
    UpdateURLForFilterChange();
});


$(document).on("click", "#filter-search-icon", function (e) {
    $("#search-filter").trigger("submit");
});

$(document).on("change", ".industry_select_picker", function (e) {
    RefreshCaseStudies();
    UpdateURLForFilterChange();
});

function ClearFilterAction() {

    if ($("#search-box").val().trim() != "") {

        if ($("#clear-filter").css("display") == "none") {
            $("#clear-filter").fadeIn();
            $("#filter-search-icon").fadeOut();
        }

    } else {
        if ($("#clear-filter").css("display") != "none") {
            $("#clear-filter").fadeOut();
            $("#filter-search-icon").fadeIn();
        }
    }
}

// search filter 
$(document).on("submit", "#search-filter", function (event) {
    ClearFilterAction();
    event.preventDefault();
    RefreshCaseStudies();
    UpdateURLForFilterChange();
});

function UpdateURLForFilterChange() {
    searchTerm = $('#search-box').val();
    industryFilter = $(".industry_select_picker").val();

    if (industryFilter != null && industryFilter != "") {

        $("h1").text($(".industry_select_picker option:selected").text() + " Videos");

        if (searchTerm != null && searchTerm != "") {

            history.pushState(window.location.pathname, null, "/videos/" + industryFilter + "?searchkey=" + searchTerm);
        }
        else {

            history.pushState(window.location.pathname, null, "/videos/" + industryFilter);

        }
    }
    else if (searchTerm != null && searchTerm != "") {

        history.pushState(window.location.pathname, null, "/videos/all?searchkey=" + searchTerm);

    } else {

        history.pushState(window.location.pathname, null, "/videos/all");

    }
}


$(window).on("popstate", function (event) {

    var URLIndustry = window.location.pathname.split('/')[3];

    $('.industry_select_picker').multiselect('select', URLIndustry);

    var queries = getQueryString();
    if (queries != "" && queries != null && queries["searchkey"] != undefined) {
        $('#search-box').val(queries["searchkey"].split(","));
    } else {
        $('#search-box').val("");
    }

    RefreshCaseStudies();

});
