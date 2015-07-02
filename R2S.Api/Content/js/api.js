/*
 * API.js
 */

var API = (function () {
  
  // Properties
  ///////////////////////////
  
  var usingMock = false;
  
  var personnel = {
        name:        "(no data)",
        rate_rank:   "(no data)",
        profile_img: "Content/img/cnrf.png"
      },
      clearance_date = "(no data)",
      last_pha       = "(no data)",
      medical_status = "(no data)",
      dental_status  = "(no data)";
  
  var personnel_mock = {
        name:        "Carina Stone",
        rate_rank:   "IS2",
        profile_img: "Content/img/sailor.png"
      },
      clearance_date_mock = "27 May 15",
      last_pha_mock       = "10 Jun 15",
      medical_status_mock = "Ready",
      dental_status_mock  = "Ready";
  
  // Public Methods
  ///////////////////////////
  
  /*
   * Set the API to use mock data
   */
  var useMockData = function (useMock) {
    if (useMock) {
      usingMock = true;
    }
  };
  
  // API calls
  
  /*
   * Get the sailor's personnel data
   */
  var getPersonnelData = function (callback) {
    if (usingMock) {
      callback(personnel_mock);
    } else {
      $.getJSON("/Personnel", function (data) {
        personnel.name = data.name;
        personnel.rate_rank = data.rate_rank;
        personnel.profile_img = data.profile_img;
        callback(personnel);
      })
      .fail(function() {
        callback(personnel);
      });
    }
  };
  
  
  /*
   * Get the sailor's clearance data
   */
  var getClearanceData = function (callback) {
    if (usingMock) {
      callback(clearance_date_mock);
    } else {
      $.getJSON("/Clearance", function (data) {
        clearance_date = data.clearance_date;
        callback(clearance_date);
      })
      .fail(function() {
        callback(clearance_date);
      });
    }
  };
  
  /*
   * Get the sailor's PHA data
   */
  var getPHAData = function (callback) {
    if (usingMock) {
      callback(last_pha_mock);
    } else {
      $.getJSON("/PHA", function (data) {
        last_pha = data.last_pha;
        callback(last_pha);
      })
      .fail(function() {
        callback(last_pha);
      });
    }
  };
  
  /*
   * Get the sailor's medical data
   */
  var getMedicalData = function (callback) {
    if (usingMock) {
      callback(medical_status_mock);
    } else {
      $.getJSON("/Medical", function (data) {
        medical_status = data.medical_status;
        callback(medical_status);
      })
      .fail(function() {
        callback(medical_status);
      });
    }
  };
  
  /*
   * Get the sailor's dental data
   */
  var getDentalData = function (callback) {
    if (usingMock) {
      callback(dental_status_mock);
    } else {
      $.getJSON("/Dental", function (data) {
        dental_status = data.dental_status;
        callback(dental_status);
      })
      .fail(function() {
        callback(dental_status);
      });
    }
  };
  
  
  // Init
  ///////////////////////////
  
  // Reveal public methods
  return {
    useMockData:      useMockData,
    getPersonnelData: getPersonnelData,
    getClearanceData: getClearanceData,
    getPHAData:       getPHAData,
    getMedicalData:   getMedicalData,
    getDentalData:    getDentalData
  };
  
})();
