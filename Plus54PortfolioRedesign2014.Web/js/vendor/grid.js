/*
* debouncedresize: special jQuery event that happens once after a window resize
*
* latest version and complete README available on Github:
* https://github.com/louisremi/jquery-smartresize/blob/master/jquery.debouncedresize.js
*
* Copyright 2011 @louis_remi
* Licensed under the MIT license.
*/
var $event = $.event,
    $special,
    resizeTimeout;

$special = $event.special.debouncedresize = {
	setup: function () {
		$(this).on("resize", $special.handler);
	},
	teardown: function () {
		$(this).off("resize", $special.handler);
	},
	handler: function (event, execAsap) {
		// Save the context
		var context = this,
			args = arguments,
			dispatch = function () {
				// set correct event type
				event.type = "debouncedresize";
				$event.dispatch.apply(context, args);
			};

		if (resizeTimeout) {
			clearTimeout(resizeTimeout);
		}

		execAsap ?
			dispatch() :
			resizeTimeout = setTimeout(dispatch, $special.threshold);
	},
	threshold: 250
};

// ======================= imagesLoaded Plugin ===============================
// https://github.com/desandro/imagesloaded

// $('#my-container').imagesLoaded(myFunction)
// execute a callback when all images have loaded.
// needed because .load() doesn't work on cached images

// callback function gets image collection as argument
//  this is the container

// original: MIT license. Paul Irish. 2010.
// contributors: Oren Solomianik, David DeSandro, Yiannis Chatzikonstantinou

// blank image data-uri bypasses webkit log warning (thx doug jones)
var BLANK = 'data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///ywAAAAAAQABAAACAUwAOw==';

$.fn.imagesLoaded = function (callback) {
	var $this = this,
		deferred = $.isFunction($.Deferred) ? $.Deferred() : 0,
		hasNotify = $.isFunction(deferred.notify),
		$images = $this.find('img').add($this.filter('img')),
		loaded = [],
		proper = [],
		broken = [];

	// Register deferred callbacks
	if ($.isPlainObject(callback)) {
		$.each(callback, function (key, value) {
			if (key === 'callback') {
				callback = value;
			} else if (deferred) {
				deferred[key](value);
			}
		});
	}

	function doneLoading() {
		var $proper = $(proper),
			$broken = $(broken);

		if (deferred) {
			if (broken.length) {
				deferred.reject($images, $proper, $broken);
			} else {
				deferred.resolve($images);
			}
		}

		if ($.isFunction(callback)) {
			callback.call($this, $images, $proper, $broken);
		}
	}

	function imgLoaded( img, isBroken ) {
		// don't proceed if BLANK image, or image is already loaded
		if ( img.src === BLANK || $.inArray( img, loaded ) !== -1 ) {
			return;
		}

		// store element in loaded images array
		loaded.push(img);

		// keep track of broken and properly loaded images
		if (isBroken) {
			broken.push(img);
		} else {
			proper.push(img);
		}

		// cache image and its state for future calls
		$.data( img, 'imagesLoaded', { isBroken: isBroken, src: img.src } );

		// trigger deferred progress method if present
		if ( hasNotify ) {
			deferred.notifyWith( $(img), [ isBroken, $images, $(proper), $(broken) ] );
		}

		// call doneLoading and clean listeners if all images are loaded
		if ($images.length === loaded.length){
			setTimeout(doneLoading);
			$images.unbind('.imagesLoaded');
		}
	}

	// if no images, trigger immediately
	if (!$images.length) {
		doneLoading();
	} else {
		$images.bind('load.imagesLoaded error.imagesLoaded', function (event) {
			// trigger imgLoaded
			imgLoaded(event.target, event.type === 'error');
		}).each(function (i, el) {
			var src = el.src;

			// find out if this image has been already checked for status
			// if it was, and src has not changed, call imgLoaded on it
			var cached = $.data(el, 'imagesLoaded');
            
			if (cached && cached.src === src) {
				imgLoaded(el, cached.isBroken);
				return;
			}

			// if complete is true and browser supports natural sizes, try
			// to check for image status manually
			if ( el.complete && el.naturalWidth !== undefined ) {
				imgLoaded( el, el.naturalWidth === 0 || el.naturalHeight === 0 );
				return;
			}

			// cached images don't fire load sometimes, so we reset src, but only when
			// dealing with IE, or image is complete (loaded) and failed manual check
			// webkit hack from http://groups.google.com/group/jquery-dev/browse_thread/thread/eee6ab7b2da50e1f
			if (el.readyState || el.complete) {
				el.src = BLANK;
				el.src = src;
			}
		});
	}

	return deferred ? deferred.promise($this) : $this;
};

var Grid = (function() {

		// list of items
	var $grid = $('#og-grid'),
		// the items
		$items = $grid.children('li'),
		// current expanded item's index
		current = -1,
		// position (top) of the expanded item
		// used to know if the preview will expand in a different row
		previewPos = -1,
		// extra amount of pixels to scroll the window
		scrollExtra = 0,
		// extra margin when expanded (between preview overlay and the next items)
		marginExpanded = 10,
		$window = $(window), winsize,
		$body = $('html, body'),
		// transitionend events
		transEndEventNames = {
			'WebkitTransition' : 'webkitTransitionEnd',
			'MozTransition' : 'transitionend',
			'OTransition' : 'oTransitionEnd',
			'msTransition' : 'MSTransitionEnd',
			'transition' : 'transitionend'
		},
		transEndEventName = transEndEventNames[Modernizr.prefixed('transition')],
		// support for csstransitions
		support = Modernizr.csstransitions,
		// default settings
		settings = {
			minHeight : 900,
			speed : 350,
			easing : 'ease'
		};

	function init( config ) {

		// the settings..
		settings = $.extend( true, {}, settings, config );

		// preload all images
		$grid.imagesLoaded( function() {

			// save item´s size and offset
			saveItemInfo( true );
			// get window´s size
			getWinSize();
			// initialize some events

			// This was commented out to be able to add items using angularjs without duplicating events
			// initEvents();

		} );

	}

	// add more items to the grid.
	// the new items need to appended to the grid.
	// after that call Grid.addItems(theItems);
	function addItems($newitems) {

		$items = $items.add($newitems);

		$newitems.each(function () {
			var $item = $(this);
			$item.data({
				offsetTop : $item.offset().top,
				height : $item.height()
			});
		});

		initEvents();

		// initItemsEvents( $newitems );

	}

	// saves the item´s offset top and height (if saveheight is true)
	function saveItemInfo( saveheight ) {
		$items.each(function () {
			var $item = $( this );
            
			$item.data('offsetTop', $item.offset().top);
            
			if(saveheight) {
				$item.data('height', $item.height());
			}
		});
	}

	function initEvents() {

		// when clicking an item, show the preview with the item´s info and large image.
		// close the item if already expanded.
		// also close if clicking on the item´s cross
		initItemsEvents( $items );

		// on window resize get the window´s size again
		// reset some values..
		$window.on('debouncedresize', function () {

			scrollExtra = 0;
			previewPos = -1;
			// save item´s offset
			saveItemInfo();
			getWinSize();
			var preview = $.data(this, 'preview');
			if(typeof preview != 'undefined') {
				hidePreview();
			}

		});

	}

	function initItemsEvents( $items ) {
		$items.on( 'click', 'span.og-close', function() {
			hidePreview();
			return false;
		} ).children( 'a' ).on( 'click', function(e) {

			var $item = $( this ).parent();
			// check if item already opened
			current === $item.index() ? hidePreview() : showPreview( $item );
			return false;

		} );
	}

	function getWinSize() {
		winsize = { width : $window.width(), height : $window.height() };
	}

	function showPreview( $item ) {

		var preview = $.data( this, 'preview' ),
			// item´s offset top
			position = $item.data( 'offsetTop' );

		scrollExtra = 0;

		// if a preview exists and previewPos is different (different row) from item´s top then close it
		if( typeof preview != 'undefined' ) {

			// not in the same row
			if( previewPos !== position ) {
				// if position > previewPos then we need to take te current preview´s height in consideration when scrolling the window
				if( position > previewPos ) {
					scrollExtra = preview.height;
				}
				hidePreview();
			}
			// same row
			else {
				preview.update( $item );
				return false;
			}

		}

		// update previewPos
		previewPos = position;
		// initialize new preview for the clicked item
		preview = $.data( this, 'preview', new Preview( $item ) );
		// expand preview overlay
		preview.open();

	}

	function hidePreview() {
		current = -1;
		var preview = $.data( this, 'preview' );
		preview.close();
		$.removeData( this, 'preview' );
	}

	// the preview obj / overlay
	function Preview( $item ) {
		this.$item = $item;
		this.expandedIdx = this.$item.index();
		this.create();
		this.update();
	}

	Preview.prototype = {
		create: function() {
			// create Preview structure:

			//top content
			this.$closePreview = $( '<span class="og-close"></span>' );
			this.$title = $( '<h3></h3>' );
			this.$category = $('<span class="category"></span>');
			this.$headcontent = $('<div class="head-conent"></div>').append(this.$title,this.$category);

			//fullimages
			this.$loading = $('<div class="og-loading"></div>');
			this.$fullimage = $('<div class="og-fullimg" style="height: 500px; width: 960px;"></div>');

			//description detail
			this.$description = $( '<p></p>' );
			this.$technologies = $( '<p></p>' );
			this.$integration = $( '<p></p>' );
			this.$description = $( '<p></p>' );

			this.$technologiesBox = $('<div class="technology"><h5>Technology</h5></div>').append(this.$technologies);
			this.$integrationBox = $('<div class="integration"><h5>Social integration</h5></div>').append(this.$integration);
			this.$detailcontent = $('<div class="detail-content"></div>').append('<h4>Overview</h4>',this.$description,this.$technologiesBox,this.$integrationBox);

			//more info
			this.$href = $( '<a href="#" target="_blank"></a>' );
			this.$sitename = $('<p class="sitename"></p>').append( this.$href);
			this.$partner = $('<span></span>');
			this.$year = $('<span></span>');
			this.$clientlogo = $('<div></div>');

			this.$partnerBox = $('<p>Partner: </p>').append(this.$partner);
			this.$yearBox = $('<p>Year: </p>').append(this.$year);
			this.$clientlogoBox = $('<div class="clientlogo"><h5>Client</h5></div>').append(this.$clientlogo);
			this.$moreinfo = $('<div class="moreinfo"><h5>More Info</h5></div>').append(this.$sitename,this.$partnerBox,this.$yearBox,this.$clientlogoBox);


			this.$details = $('<div class="og-details"></div>').append(this.$detailcontent, this.$moreinfo);
			this.$previewInner = $( '<div class="og-expander-inner"></div>' ).append( this.$closePreview,this.$headcontent, this.$fullimage, this.$details);
			this.$previewEl = $( '<div class="og-expander"></div>' ).append( this.$previewInner );

			// append preview element to the item
			this.$item.append(this.getEl());
            
			// set the transitions for the preview and the item
			if( support ) {
				this.setTransition();
			}
            
            //Slick Init
            $('.og-fullimg').slick({
                centerMode: true,
                slidesToShow: 1,
                arrows: true
            });
		},
        
		update: function( $item ) {

			if($item) {
				this.$item = $item;
			}

			// if already expanded remove class "og-expanded" from current item and add it to new item
			if( current !== -1 ) {
				var $currentItem = $items.eq(current);
				$currentItem.removeClass('og-expanded');
				$('#og-grid li').removeClass('inactive');

				this.$item.addClass('og-expanded');
				this.$item.siblings().addClass('inactive');
				// position the preview correctly
				this.positionPreview();
			}

			// update current value
			current = this.$item.index();

			// update preview´s content
			var sitename = this.$item.find('a').attr('href'),
                $itemEl = this.$item.children( 'a' ),
				eldata = {
                    title:          $itemEl.data('title'),
                    category:       $itemEl.data('category'), 
                    description:    $itemEl.data('description'),
                    technologies:   $itemEl.data('technologies'),
                    integration:    $itemEl.data('integration'),
                    href:           $itemEl.attr('href'),
                    partner:        $itemEl.data('partner'), 
                    year:           $itemEl.data('year'), 
                    clientlogo:     $itemEl.data('clientlogo'), 
                    largesrc:       $itemEl.data('largesrc')
				};
            
            var technologiesImages = "",
                socialImages = "",
                sliderImages = "";
            
            //For every image on Technologies, create the HTML
            eldata.technologies.forEach(function (techUrl){
                technologiesImages += '<img src="' + techUrl + '" />';
            });
            
            //For every image on Social, create the HTML
            eldata.integration.forEach(function (socialUrl){
                socialImages += '<img src="' + socialUrl + '" />';
            });
            
            //For every image on Large, create the HTML
            eldata.largesrc.forEach(function (largeUrl){
                //sliderImages += '<div style="height: 500px;"><img src="' + largeUrl + '" /></div>';
                
                //Add image to Slick
                $('.og-fullimg', this.$fullimage).slickAdd('<div style="height: 500px;"><img src="' + largeUrl + '" /></div>');    
            });
            
            //Bind data to Template
			this.$title.html(eldata.title);
			this.$category.html('<img src="' + eldata.category + '" />');
			this.$description.html(eldata.description);
			this.$technologies.html(technologiesImages);
			this.$integration.html(socialImages);
			this.$href.attr('href', eldata.href);
			this.$sitename.html('<a href="' + eldata.href + '" target="_blank">' + sitename + '</a>');
			this.$partner.html(eldata.partner);
			this.$year.html(eldata.year);			
			this.$clientlogo.html('<img src="' + eldata.clientlogo + '" />');
            
            //this.$fullimage.html(sliderImages);

			var self = this;

			//Remove the current image in the preview
			if(typeof self.$largeImg != 'undefined') {
				self.$largeImg.remove();
			}
            
			// preload large image and add it to the preview
			// for smaller screens we don´t display the large image (the media query will hide the fullimage wrapper)
			if(self.$fullimage.is(':visible')) {
				this.$loading.show();
                
				$('<img/>').load(function () {
					var $img = $(this);
                    
					if($img.attr('src') === self.$item.children('a').data('largesrc')) {
						self.$loading.hide();
						self.$fullimage.find('img').remove();
						self.$largeImg = $img.fadeIn(350);
						self.$fullimage.append(self.$largeImg);
                        
                        
					}
				}).attr('src', eldata.largesrc);
			}
		},
        
		open: function () {

			setTimeout( $.proxy( function () {
				// set the height for the preview and the item
				this.setHeights();
				// scroll to position the preview in the right place
				this.positionPreview();
                
                
			}, this), 25);

		},
        
		close: function () {

			var self = this,
				onEndFn = function () {
					if(support) {
						$(this).off(transEndEventName);
					}
                    
					self.$item.removeClass('og-expanded');
					self.$item.siblings().removeClass('inactive');
					self.$previewEl.remove();
                    
                    //Destroy slider
                    //$('.og-fullimg').unslick();
				};

			setTimeout($.proxy(function () {

				if(typeof this.$largeImg !== 'undefined') {
					this.$largeImg.fadeOut('fast');
				}
                
				this.$previewEl.css('height', 0);
				// the current expanded item (might be different from this.$item)
				var $expandedItem = $items.eq( this.expandedIdx );
				$expandedItem.css('height', $expandedItem.data( 'height' )).on(transEndEventName, onEndFn);

				if(!support) {
					onEndFn.call();
				}

			}, this), 25);

			return false;
		},
        
		calcHeight: function() {

			var heightPreview = winsize.height - this.$item.data('height') - marginExpanded,
				itemHeight = winsize.height;

			if( heightPreview < settings.minHeight ) {
				heightPreview = settings.minHeight;
				itemHeight = settings.minHeight + this.$item.data('height') + marginExpanded;
			}

			this.height = heightPreview;
			this.itemHeight = itemHeight;
		},
        
		setHeights : function() {

			var self = this,
				onEndFn = function() {
					if( support ) {
						self.$item.off( transEndEventName );
					}
					self.$item.addClass('og-expanded');
					self.$item.siblings().addClass('inactive');
				};

			this.calcHeight();
			this.$previewEl.css( 'height', this.height );
			this.$item.css( 'height', this.itemHeight ).on(transEndEventName, onEndFn);

			if( !support ) {
				onEndFn.call();
			}
		},
        
		positionPreview: function () {
			// scroll page
			// case 1 : preview height + item height fits in window´s height
			// case 2 : preview height + item height does not fit in window´s height and preview height is smaller than window´s height
			// case 3 : preview height + item height does not fit in window´s height and preview height is bigger than window´s height
			var position = this.$item.data( 'offsetTop' ),
				previewOffsetT = this.$previewEl.offset().top - scrollExtra,
				scrollVal = this.height + this.$item.data( 'height' ) + marginExpanded <= winsize.height ? position : this.height < winsize.height ? previewOffsetT - ( winsize.height - this.height ) : previewOffsetT;

			$body.animate( { scrollTop : scrollVal }, settings.speed );

		},
        
		setTransition : function () {
			this.$previewEl.css( 'transition', 'height ' + settings.speed + 'ms ' + settings.easing );
			this.$item.css( 'transition', 'height ' + settings.speed + 'ms ' + settings.easing );
		},
        
		getEl: function() {
			return this.$previewEl;
		}
	}

	return {
		init : init,
		addItems : addItems
	};
    
    

})();
