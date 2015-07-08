using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using R2S.Api.Models;
using System.Diagnostics;


//using System.Boolean;


namespace R2S.Api.Controllers
{
    public class OpportunitiesController : Controller
    {
        // Pre-canned syntax
        private const string QUERY_START = "SELECT ";
        private const string QUERY_WHERE = " WHERE ";
        private const string QUERY_FROM = " FROM ALL_VIEW ";
        private const string QUERY_END   = ";";

        // TODO: Limit rows returned in prototype code
        private const string QUERY_LIMIT = " top 50 ";

        // Column names which must match column names in Database
        private const string COLUMN_ADVERTISEMENT_ID    = "AdvertisementID";
        private const string COLUMN_MINIMUM_RANK        = "MinimumRank";
        private const string COLUMN_MAXIMUM_RANK        = "MaximumRank";
        private const string COLUMN_DESIGNATOR          = "Designator";
        private const string COLUMN_NOBC                = "NOBC";
        private const string COLUMN_RATING              = "Rating";
        private const string COLUMN_NEC                 = "NEC";
        private const string COLUMN_MISSION_NAME        = "MissionName";
        private const string COLUMN_MISSION_REPORT_DATE = "MissionReportDate";
        private const string COLUMN_MISSION_END_DATE    = "MissionEndDate";
        private const string COLUMN_CONUS_OR_OCONUS     = "ConusOconusID"; 
        private const string COLUMN_CITY                = "City";
        private const string COLUMN_STATE               = "State";  
        private const string COLUMN_COUNTRY             = "Country";


        private List<string> DatabaseColumnsToQuery;

        List<string> debugMessages = new List<String>();

        public OpportunitiesController()
        {
            DatabaseColumnsToQuery = new List<string>();

            // Add all the columns that we want to query for each opportunity
            DatabaseColumnsToQuery.Add(COLUMN_ADVERTISEMENT_ID);
            DatabaseColumnsToQuery.Add(COLUMN_MINIMUM_RANK);
            DatabaseColumnsToQuery.Add(COLUMN_MAXIMUM_RANK);
            DatabaseColumnsToQuery.Add(COLUMN_DESIGNATOR);
            DatabaseColumnsToQuery.Add(COLUMN_NOBC);
            DatabaseColumnsToQuery.Add(COLUMN_RATING);
            DatabaseColumnsToQuery.Add(COLUMN_NEC);
            DatabaseColumnsToQuery.Add(COLUMN_MISSION_NAME);
            DatabaseColumnsToQuery.Add(COLUMN_MISSION_REPORT_DATE);
            DatabaseColumnsToQuery.Add(COLUMN_MISSION_END_DATE);
            DatabaseColumnsToQuery.Add(COLUMN_CONUS_OR_OCONUS);
            DatabaseColumnsToQuery.Add(COLUMN_CITY);
            DatabaseColumnsToQuery.Add(COLUMN_STATE);
            DatabaseColumnsToQuery.Add(COLUMN_COUNTRY);

        }

        // GET: Opportunties
        public ActionResult Index()
        {

/*
            System.Diagnostics.Debug.WriteLine("DEBUG: OpportunitesController.Index() Called");
            System.Diagnostics.Trace.WriteLine("DEBUG: OpportunitesController.Index() Called");
            Trace.TraceWarning("XXX");
            Trace.WriteLine("DEBUG: OpportunitesController.Index() Called");

            debugMessages.Add(" OpportunitesController.Index() Called");
            //Response.Write("DEBUG: OpportunitesController.Index() Called");
*/
            // Initialize list to hold all opportunities that match from database
            var opportunities = new List<OpportunityModel>();
/*
            // Will hold the final query string to send to database
            string queryString = QUERY_START;

            // TODO: Limit rows returned in prototype code
            queryString += QUERY_LIMIT;

            // For each column to query, add it to query string with needed "," seperators 
            int numbersOfColumns = DatabaseColumnsToQuery.Count;
            int i = 1;

            foreach (string columnToQuery in DatabaseColumnsToQuery)
            {

                //queryString += "LTRIM(RTRIM(" + columnToQuery + "))";
                queryString += columnToQuery;
                
                if (i < numbersOfColumns) {
                    queryString += ", ";
                }

                i++;
            }

            // Finish query string
            queryString += QUERY_FROM;
            queryString += QUERY_END;

            // Get the needed database connection
            System.Configuration.Configuration rootwebconfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/Mohtisham");
            System.Configuration.ConnectionStringSettings constring;
            constring = rootwebconfig.ConnectionStrings.ConnectionStrings["OpportunitiesConnectionString"];

            using (SqlConnection connection =
                   new SqlConnection(constring.ConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = queryString;

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        // Parse out the columns for each row obtained and do any needed combining of data before it will be sent to view
                        string advertisementID = reader.IsDBNull(DatabaseColumnsToQuery.IndexOf(COLUMN_ADVERTISEMENT_ID)) ? "" : reader.GetString(DatabaseColumnsToQuery.IndexOf(COLUMN_ADVERTISEMENT_ID));
                        string missionName = reader.IsDBNull(DatabaseColumnsToQuery.IndexOf(COLUMN_MISSION_NAME)) ? "" : reader.GetString(DatabaseColumnsToQuery.IndexOf(COLUMN_MISSION_NAME)).Trim();
                        string minimumRank = reader.IsDBNull(DatabaseColumnsToQuery.IndexOf(COLUMN_MINIMUM_RANK)) ? "" : reader.GetString(DatabaseColumnsToQuery.IndexOf(COLUMN_MINIMUM_RANK)).Trim();
                        string maximumRank = reader.IsDBNull(DatabaseColumnsToQuery.IndexOf(COLUMN_MAXIMUM_RANK)) ? "" : reader.GetString(DatabaseColumnsToQuery.IndexOf(COLUMN_MAXIMUM_RANK)).Trim();
                        string rankRange = minimumRank + "-" + maximumRank;
                        //string startDate = reader[DatabaseColumnsToQuery.IndexOf(COLUMN_MISSION_REPORT_DATE)].ToString();
                        //string endDate = reader[DatabaseColumnsToQuery.IndexOf(COLUMN_MISSION_END_DATE)].ToString();
                        DateTime startDate = reader.GetDateTime(DatabaseColumnsToQuery.IndexOf(COLUMN_MISSION_REPORT_DATE));
                        DateTime endDate = reader.GetDateTime(DatabaseColumnsToQuery.IndexOf(COLUMN_MISSION_END_DATE));

                        // Combine location data into one string to simplify view process
                        // TODO: Better to have the view worry about this?
                        int isConus = reader.GetInt16(DatabaseColumnsToQuery.IndexOf(COLUMN_CONUS_OR_OCONUS));
                        string location;
                        string locationFirstPart = reader.IsDBNull(DatabaseColumnsToQuery.IndexOf(COLUMN_CITY)) ? "" : reader.GetString(DatabaseColumnsToQuery.IndexOf(COLUMN_CITY));
                        if (String.IsNullOrEmpty(locationFirstPart))
                        {
                            locationFirstPart = "";
                        }

                        string locationSecondPart;
                        if (isConus == 1)
                        {
                            locationSecondPart = reader.IsDBNull(DatabaseColumnsToQuery.IndexOf(COLUMN_STATE)) ? "" : reader.GetString(DatabaseColumnsToQuery.IndexOf(COLUMN_STATE));
                        }
                        else
                        {
                            locationSecondPart = reader.IsDBNull(DatabaseColumnsToQuery.IndexOf(COLUMN_COUNTRY)) ? "" : reader.GetString(DatabaseColumnsToQuery.IndexOf(COLUMN_COUNTRY));
                        }

                        if (String.IsNullOrEmpty(locationSecondPart))
                        {
                            locationSecondPart = "";
                        }
                        location = locationFirstPart.Trim() + ", " + locationSecondPart.Trim();

                        // Combine some qualification data into one string to simplify view process
                        // TODO: Better to have the view worry about this?
                        string designator = reader.IsDBNull(DatabaseColumnsToQuery.IndexOf(COLUMN_DESIGNATOR)) ? "" : reader.GetString(DatabaseColumnsToQuery.IndexOf(COLUMN_DESIGNATOR));
                        string nobc = reader.IsDBNull(DatabaseColumnsToQuery.IndexOf(COLUMN_NOBC)) ? "" : reader.GetString(DatabaseColumnsToQuery.IndexOf(COLUMN_NOBC)).Trim();
                        string rating = reader.IsDBNull(DatabaseColumnsToQuery.IndexOf(COLUMN_RATING)) ? "" : reader.GetString(DatabaseColumnsToQuery.IndexOf(COLUMN_RATING)).Trim();
                        string nec = reader.IsDBNull(DatabaseColumnsToQuery.IndexOf(COLUMN_NEC)) ? "" : reader.GetString(DatabaseColumnsToQuery.IndexOf(COLUMN_NEC)).Trim();

                        string qualificationSummary = "";
                        if (String.IsNullOrEmpty(designator))
                        {
                            if (String.IsNullOrEmpty(rating))
                            {
                                // Do nothing
                            }
                            else
                            {
                                if (String.IsNullOrEmpty(nec))
                                {
                                    // Only rating is not null, so only show rating
                                    qualificationSummary = rating;
                                }
                                else
                                {
                                    // Must be enlisted billet with both rating and nec present, so will show both
                                    qualificationSummary = rating + "(" + nec + ")";
                                }
                            }
                        }
                        // else designator present, so must be officer opportunity.  Therefore only care about seeing if NOBC is present
                        else
                        {
                            if (String.IsNullOrEmpty(nobc))
                            {
                                qualificationSummary = designator;
                            }
                            else
                            {
                                qualificationSummary = designator + "(" + nobc + ")";

                            }
                        }


                        var opportunity = new OpportunityModel() { AdvertisementID = advertisementID, RankRange = rankRange, QualificationSummary = qualificationSummary, MissionName = missionName, ReportDate = startDate, EndDate = endDate, Location = location };
                        opportunities.Add(opportunity);

                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            ViewData["DebugMessages"] = debugMessages;
*/
            var opportunity = new OpportunityModel() { AdvertisementID = "ID_TEST", RankRange = "RANK_TEST", QualificationSummary = "QUAL_TEST", MissionName = "MISSION_TEST", ReportDate = DateTime.Now, EndDate = DateTime.Now, Location = "LOCATION_TEST" };
            opportunities.Add(opportunity);
 
            return this.Json(opportunities, JsonRequestBehavior.AllowGet);

            //return View();
        }
    }
}