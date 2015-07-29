/*
 * API.js
 */

var API = (function () {
  
  // Properties
  ///////////////////////////
  
  var baseURL = "",
      API_URL = "/apps/r2s",
      API_URL_MOCK = "Content/API";
  
  var usingMock = false;
  
  var personnel = {
        name:        "(no data)",
        rate_rank:   "(no data)",
        profile_img: "Content/img/cnrf.png"
      },
      opportunities = [],
      clearance = {
        date: "(no data)"
      },
      pha = {
        last_pha: "(no data)"
      },
      medical = {
        status: "(no data)"
      },
      dental = {
        status: "(no data)"
      };
  
  // Public Methods
  ///////////////////////////
  
  /*
   * Set the API to use mock data
   */
  var useMockData = function (useMock) {
    if (useMock) {
      usingMock = true;
      baseURL = API_URL_MOCK;
    }
  };
  
  var isUsingMock = function () {
    return usingMock;
  };
  
  // API calls
  
  /*
   * Get the sailor's personnel data
   */
  var getPersonnelData = function (callback) {
    $.getJSON(baseURL + "/Personnel", function (data) {
      personnel.name = data.name;
      personnel.rate_rank = data.rate_rank;
      personnel.profile_img = data.profile_img;
      callback(personnel);
    })
    .fail(function() {
      callback(personnel);
    });
  };
  
  /*
   * Get opportunities data
   */
  var getOpportunitiesData = function (callback) {


	  // Failed
      //var strMethodUrl = '@Url.Action(  "Index", "Opportunities")';

      // Worked (but only on localhost and not Test/QA which resulted in this URL https://private.test.nrh.navyreserve.navy.mil/apps/r2s/apps/r2s/Opportunities/Index
      // var strMethodUrl = "./apps/r2s/Opportunities/Index"
      //var strMethodUrl = "./apps/r2s/Opportunities/DummyData"

      //var strMethodUrl = "." + baseURL + "/Opportunities/Index";

      // Expected to work on Test/QA but does not on localhost
      var strMethodUrl = "./Opportunities/Index";
      //var strMethodUrl = "./Opportunities/DummyData";

      // $.getJSON("../" + baseURL + "/Opportunities", function (data) {
      $.getJSON(strMethodUrl, function (data) {

      opportunities = data;
      callback(opportunities);
    })
    .fail(function() {
      callback(opportunities);
    });
  };
  
  /*
   * Get the sailor's clearance data
   */
  var getClearanceData = function (callback) {
    $.getJSON(baseURL + "/Clearance", function (data) {
      clearance.date = data.date;
      callback(clearance);
    })
    .fail(function() {
      callback(clearance);
    });
  };
  
  /*
   * Get the sailor's PHA data
   */
  var getPHAData = function (callback) {
    $.getJSON(baseURL + "/PHA", function (data) {
      pha.last_pha = data.last_pha;
      callback(pha);
    })
    .fail(function() {
      callback(pha);
    });
  };
  
  /*
   * Get the sailor's medical data
   */
  var getMedicalData = function (callback) {
    $.getJSON(baseURL + "/Medical", function (data) {
      medical.status = data.status;
      callback(medical);
    })
    .fail(function() {
      callback(medical);
    });
  };
  
  /*
   * Get the sailor's dental data
   */
  var getDentalData = function (callback) {
    $.getJSON(baseURL + "/Dental", function (data) {
      dental.status = data.status;
      callback(dental);
    })
    .fail(function() {
      callback(dental);
    });
  };
  
  
  // Init
  ///////////////////////////
  
  baseURL = API_URL;
  
  // Reveal public methods
  return {
    useMockData:          useMockData,
    isUsingMock:          isUsingMock,
    getPersonnelData:     getPersonnelData,
    getOpportunitiesData: getOpportunitiesData,
    getClearanceData:     getClearanceData,
    getPHAData:           getPHAData,
    getMedicalData:       getMedicalData,
    getDentalData:        getDentalData
  };
  
})();
