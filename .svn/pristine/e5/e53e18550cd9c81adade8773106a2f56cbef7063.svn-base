jQuery(document).ready(function($) {
  'use strict';

  $(document).foundation();
  $('form').on('invalid.fndtn.abide', function() {
    $('#error').show();
  });

  //search Mobile binding

  $('#searchMobile').bind('keypress', function(e) {
    if (e.which === 13) {
      $('#searchSubmit').trigger('click');
      $('#sidebarButton').trigger('click');
      return false;
    }
  });


});
