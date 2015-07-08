/*
 * App.js
 */

var App = (function () {
  
  // Properties
  ///////////////////////////
  
  
  // Private Methods
  ///////////////////////////
  
  /*
   * Hides all views.
   */
  var hideViews = function () {
    hideMobileMenu();
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
    case 'opportunities':
      opportunitiesController();
      break;
    case 'api':
      apiController();
      break;
    }
    
    // Show the view
    $('#' + view).show();
    
    // Add the view to the browser history
    history.pushState({}, "", "#" + view);
    
    // Scroll to top after view change
    setTimeout(function() {
      $(document.body).scrollTop(0);
    }, 10);
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
  
  var opportunitiesController = function () {
    API.getOpportunitiesData(function (data) {

        var html = "";

        // START: Mike's multi-column view

        // Format header row
        // TODO: This should use common css style to applied across entire interface
        // TODO: Not sure why background color is not working
        //html += "<tbody align=\"center\" style=\"font-weight:bold; background-color:#9D9696 bgcolor:#9D9696\">";

        // Table titles must match order of cells below in for loop
        html += "<tr>";
        html += "<td>ID</td>";
        html += "<td>Ranks</td>";
        html += "<td>Qualifications</td>";
        html += "<td>Mission Name</td>";
        html += "<td>Report Date</td>";
        html += "<td>End Data</td>";
        html += "<td>Location</td>";
        html += "</tr>";

        // Close out format for header row
       // html += "</tbody>"


        $.each(data, function (i, val) {
            // var opportunity = new OpportunityModel() { AdvertisementID = advertisementID, RankRange = rankRange, QualificationSummary = qualificationSummary, MissionName = missionName, ReportDate = startDate, EndDate = endDate, Location = location };

            html += "<tr>";
            html += "<td>" + val.AdvertisementID + "</td>";
            html += "<td>" + val.RankRange + "</td>";
            html += "<td>" + val.QualificationSummary + "</td>";
            html += "<td>" + val.MissionName + "</td>";

            // TODO: Should make this a function
            var dateObject = new Date(parseInt(val.ReportDate.substring(6)));
            var dateString = dateObject.toLocaleDateString();
            html += "<td>" + dateString + "</td>";

            // TODO: Should make this a function
            dateObject = new Date(parseInt(val.EndDate.substring(6)));
            dateString = dateObject.toLocaleDateString();
            html += "<td>" + dateString + "</td>";

            html += "<td>" + val.Location + "</td>";
            html += "</tr>";
        });
       // html += "</table>";





        // START: Below is Tom's single column view
        /*


      
      $.each(data, function(i, val) {
        html += "<tr><td></td><td></td></tr>";
        html += "<tr>";
        html += "<td>Advertisement ID</td>";
        html += "<td>" + val.AdvertisementID + "</td>";
        html += "</tr>";
        
        html += "<tr>";
        html += "<td>Rank Range</td>";
        html += "<td>" + val.RankRange + "</td>";
        html += "</tr>";
        
        html += "<tr>";
        html += "<td>Qualification Summary</td>";
        html += "<td>" + val.QualificationSummary + "</td>";
        html += "</tr>";
        
        html += "<tr>";
        html += "<td>Mission Name</td>";
        html += "<td>" + val.MissionName + "</td>";
        html += "</tr>";
        
        html += "<tr>";
        html += "<td>Report Date</td>";
        html += "<td>" + val.ReportDate + "</td>";
        html += "</tr>";
        
        html += "<tr>";
        html += "<td>End Date</td>";
        html += "<td>" + val.EndDate + "</td>";
        html += "</tr>";
        
        html += "<tr>";
        html += "<td>Location</td>";
        html += "<td>" + val.Location + "</td>";
        html += "</tr>";
      });
      
      */
      $('#opportunities-table').html(html);
    });
  };
  
  var apiController = function () {
    if (API.isUsingMock()) {
      $('#api-using-mock-data').html("Yes");
    } else {
      $('#api-using-mock-data').html("No");
    }
    
    API.getPersonnelData(function (data) {
      $('#api-personnel').html(JSON.stringify(data, null, 2));
    });
    API.getOpportunitiesData(function (data) {
      $('#api-opportunities').html(JSON.stringify(data, null, 2));
    });
    API.getClearanceData(function (data) {
      $('#api-clearance').html(JSON.stringify(data, null, 2));
    });
    API.getPHAData(function (data) {
      $('#api-pha').html(JSON.stringify(data, null, 2));
    });
    API.getMedicalData(function (data) {
      $('#api-medical').html(JSON.stringify(data, null, 2));
    });
    API.getDentalData(function (data) {
      $('#api-dental').html(JSON.stringify(data, null, 2));
    });
  };
  
  // Public Methods
  ///////////////////////////
  
  /*
   * An example public method.
   */
  var publicMethod = function () {};
  
  // Init
  ///////////////////////////
  
  // When the hash fragment changes
  $(window).on('hashchange', function(e) {
    showView(window.location.hash.slice(1));
  });
  
  // Set API to use remote or local data
  // Also could check `window.location.host == "pages.18f.gov"`
  if (getUrlParamValue('mock') == 'true') {
    API.useMockData(true);
  }
  
  // Load data from API on load
  API.getPersonnelData(function (data) {
    $('#name').html(data.name);
    $('#rate-rank').html(data.rate_rank);
    $('#profile-img').attr('src', data.profile_img);
  });
  
  API.getClearanceData(function (data) {
    $('#clearance-date').html(data.date);
  });
  API.getPHAData(function (data) {
    $('#last-pha').html(data.last_pha);
  });
  API.getMedicalData(function (data) {
    $('#medical-status').html(data.status);
  });
  API.getDentalData(function (data) {
    $('#dental-status').html(data.status);
  });
  
  // Read hash from URL, show current view
  var hash = window.location.hash.replace('#', '');
  if (hash == '') {
    showView('home');
  } else {
    showView(hash);
  }
  
  // Reveal public methods
  return {
    publicMethod: publicMethod,
    showView: showView
  };
  
})();
