/*
 * App.js
 */

var App = (function () {
  
  // Properties
  ///////////////////////////
  
  
  // Private Methods
  ///////////////////////////
  
  /*
   * An example private method.
   */
  var privateMethod = function () {};
  
  /*
   * Hides all views.
   */
  var hideViews = function () {
    $('.view').hide();
  };
  
  /*
   * Shows a given view.
   */
  var showView = function (view) {
    hideViews();
    
    // Run controllers for given views
    switch (view) {
    case 'home':
      homeController();
      break;
    case 'orders':
      ordersController();
      break;
    default:
      // Set a default case
    }
    
    $('#' + view).show();
    window.location.hash = view;
    window.scrollTo(0, 0);
  };
  
  /*
   * Gets a URL parameter value from key.
   */
  var getUrlParamValue = function (key) {
    var pageUrl = window.location.search.substring(1);
    var paramPairs = pageUrl.split('&');
    
    for (var i = 0; i < paramPairs.length; i++) {
      var pair = paramPairs[i].split('=');
      if (pair[0] == key) {
        return pair[1];
      }
    }
  };
  
  /*
   * Shows a partial view.
   */
  var showPartial = function (partial) {
    $('#' + partial).show();
  };
  
  /*
   * Hides a partial view.
   */
  var hidePartial = function (partial) {
    $('#' + partial).hide();
  };
  
  // Controllers
  
  var homeController = function () {
    if (getUrlParamValue('hardhold') == 'true') {
      showPartial('home-hard-hold');
    } else {
      hidePartial('home-hard-hold');
    }
  };
  
  var ordersController = function () {
    if (getUrlParamValue('hardhold') == 'true') {
      showPartial('orders-hard-hold');
      $('#request-orders-link').hide();
    } else {
      hidePartial('orders-hard-hold');
      $('#request-orders-link').show();
    }
  }
  
  // Public Methods
  ///////////////////////////
  
  /*
   * An example public method.
   */
  var publicMethod = function () {};
  
  // Init
  ///////////////////////////
  
  // Set button bindings
  
  $('#btn-login').on('click', function () {
    showView('home');
  });
  
  $('#btn-request-orders-submit').on('click', function () {
    swal({
      title: "Orders submitted!",
      type: 'success',
      confirmButtonColor: '#084D8A'
    }, function () {
      showView('home');
    });
  });
  
  $('#btn-request-orders-cancel, #request-orders-back').on('click', function () {
    swal({
      title: "Cancel new orders request?",
      type: 'warning',
      showCancelButton: true,
      cancelButtonText: 'No',
      confirmButtonColor: '#DD6B55',
      confirmButtonText: 'Yes'
    }, function () {
      showView('home');
    });
  });
  
  $('#btn-modify-orders-submit').on('click', function () {
    swal({
      title: "Orders modified!",
      type: 'success',
      confirmButtonColor: '#084D8A'
    }, function () {
      showView('view-orders');
    });
  });
  
  $('#btn-modify-orders-cancel, #modify-orders-back').on('click', function () {
    swal({
      title: "Cancel modifying these orders?",
      type: 'warning',
      showCancelButton: true,
      cancelButtonText: 'No',
      confirmButtonColor: '#DD6B55',
      confirmButtonText: 'Yes'
    }, function () {
      showView('view-orders');
    });
  });
  
  // Read hash from URL, show current view
  var hash = window.location.hash.replace('#', '');
  if (hash == '') {
    $('#login').show();
  } else {
    showView(hash);
  }
  
  // Reveal public methods
  return {
    publicMethod: publicMethod,
    showView: showView
  };
  
})();
