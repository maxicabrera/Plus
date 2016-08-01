/*globals jQuery,console,alert,Handlebars*/
jQuery(document).ready(function ($) {
    'use strict';

    var portfolio;
    
    //Create a namespace for the site
    portfolio = portfolio || {};
    
    portfolio.resultsUrl = "./data/results.txt";

    //Windows Size
    portfolio.windowsize = $(window).width();

    portfolio.globalFunctions = {

        init: function () {
            this.example();
        },

        example : function () {

            //alert('lala');
        }

    };

    
    portfolio.search = {
        
        init: function () {
            
            this.showFilters();
            
        },
        
        showFilters: function () {
            var titleElms = $('.filter-title'),
                filterItems = $('.filter-item'),
                contentElms = $('.filter-content'),
                submitElms = $('.filter-apply'),
                allTypeElm = $('.all-type-title'),
                allTechElm = $('.all-tech-title');
            
            titleElms.on('click', function () {
                var titleData = $(this).data('content'),
                    filterContent = $(titleData);
                
                //If Mobile - Show accordion menu
                if (portfolio.windowsize < 767) {
                    //Change the title style
                    $(this).toggleClass('active');
                    
                    //Show the request content
                    filterContent.slideToggle();

                    //Change others selected titles
                    titleElms.not($(this)).removeClass('active');
                    
                    //Hide others open contents
                    contentElms.not(filterContent).slideUp();
                }
            });
            
            filterItems.on('click', function () {
                var itemCheck = $(this).siblings(".filter-check"),
                    allTypeTitle = $('.all-type-title'),
                    allTypeCheck = $('.all-type-check'),
                    allTechTitle = $('.all-tech-title'),
                    allTechCheck = $('.all-tech-check');

                //Change the item style
                $(this).toggleClass('active');
                
                //Uncheck "all" item
                if ($(this).hasClass('type-title')) {
                    //Reset the "All" check
                    allTypeCheck.attr('checked', false);

                    //Reset the "All" title
                    allTypeTitle.removeClass('active');
                } else if ($(this).hasClass('tech-title')) {
                    //Reset the "All" check
                    allTechCheck.attr('checked', false);

                    //Reset the "All" title
                    allTechTitle.removeClass('active');
                }
            });
            
            submitElms.on('click', function () {
                var filtersForm = $("#form-filters input:checkbox:checked"),
                    applyTitle = $(this).data("title"),
                    titleElm = $(applyTitle),
                    formData = filtersForm.serializeArray();
                
                if (formData.length > 0) {
                    //Load search data from checkboxes
                    portfolio.search.loadData(formData);

                    console.log(formData);
                    
                    //Toggle father title
                    titleElm.trigger("click");
                } else {
                    //Toggle father title
                    titleElm.trigger("click");
                    
                    //Show error message
                    portfolio.search.showMessage("Select almost one filter");
                }
            });
            
            allTypeElm.on('click', function () {
                var typeItems = $('.type-check'),
                    typeTitles = $('.type-title');
                
                //Reset selected Type Items
                typeItems.attr('checked', false);
                
                //Reset all titles except this
                typeTitles.not($(this)).removeClass('active');
            });
            
            allTechElm.on('click', function () {
                var techChecks = $('.tech-check'),
                    techTitles = $('.tech-title');
                
                //Reset selected Tech Items
                techChecks.attr('checked', false);
                
                //Reset all titles except this
                techTitles.not($(this)).removeClass('active');
            });
            
        },
        
        loadData: function (formData) {
            var content = $('#search-content'),
                gridElm = $("#grid-template").html(),
                gridTemplate,
                resultsCount = $('.results-count');
            
            $.ajax({
                url: portfolio.resultsUrl,
                //method: 'post',
                dataType: 'json',
                crossDomain: true,
                contentType: 'application/json',
                data: JSON.stringify(formData),
                error: function (xhr, status, error) {
                    portfolio.search.showMessage("Service Error");
                },
                success: function (data) {
                    var count = data.count;
                    
                    if (count > 0) {
                        //Hide previus messages
                        portfolio.search.hideMessage();
                        
                        //Update Counter
                        resultsCount.html(count + " results");
                        
                        //Create Template
                        gridTemplate = Handlebars.compile(gridElm);
                        
                        //Generate item and load on Content
                        content.html(gridTemplate({ items: data.results }));
                    } else {
                        //Restar Counter
                        resultsCount.html("0 results");
                        
                        //Show error message
                        portfolio.search.showMessage("No matches found");
                    }
                }
            });
        },
        
        showMessage: function (oneMessage) {
            var content = $('#search-content'),
                messageElm = $("#message-template").html(),
                messageTemplate = Handlebars.compile(messageElm);
            
            //Generate Message and load on Content
            content.html(messageTemplate({ message: oneMessage }));
        },
        
        hideMessage: function () {
            var messageBox = $('.message-box');

            messageBox.hide();
        }
        
    };
    
    
    portfolio.validate = {

        init: function () {
            this.login();
        },

        login: function () {
            
        }

    };


    portfolio.mobileFunctions = {

        init: function () {
            this.shownav();
        },
        
        shownav : function () {
        	
        }


    };

    //=====================================
    //           Initialize!!
    //=====================================

    portfolio.mobileFunctions.init();
    
    portfolio.search.init();
    
    if (portfolio.windowsize < 767) {
        console.log('I am Mobile');
    }


   

});




