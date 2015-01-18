using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using GEPlugin;
using System.Security.Permissions;

namespace DB_Yelp
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class Form1 : Form
    {
        private const string PLUGIN_URL = @"http://earth-api-samples.googlecode.com/svn/trunk/demos/desktop-embedded/pluginhost.html";
        public IGEPlugin googleEarth = null;
        MySqlConnection sqlConnection = new MySqlConnection();
        double mapLat, mapLong, mapAlt, mapRange, mapID = 0.0;
        int maxRows = 20;
        int iconIDCount = 0;
        List<KmlPlacemarkCoClass> icons = new List<KmlPlacemarkCoClass>();
        BackgroundWorker bw = new BackgroundWorker();
        int previousQuery = -1;
        private Form loadForm = new Form();


        public Form1()
        {
            // "Server=ServerName;Database=DataBaseName;UID=username;Password=password"
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            
            sqlConnection.ConnectionString = "Server=localhost; Database=yelp; UID=root; Password=sashathedog";
            //sqlConnection.Open();
            loadComboBox();

            GEBrowser.Navigate(PLUGIN_URL);
            GEBrowser.ObjectForScripting = this;

            Bitmap bmp = new Bitmap("../../YelpIcon.png");
            this.Icon = Icon.FromHandle(bmp.GetHicon());
            
            //createLoadForm();
        }


        private void createLoadForm()
        {
            loadForm = new Form();
            loadForm.Height = 200;
            loadForm.Width = 400;
            loadForm.Text = "Loading Query";
            loadForm.BackColor = Color.DarkRed;
            Label lbl = new Label();
            lbl.ForeColor = Color.Silver;
            lbl.Font = new System.Drawing.Font("Times New Roman", 16);
            lbl.Location = new Point(100, 60);
            lbl.Width = 300;
            lbl.Height = 150;
            lbl.Text = "Loading Query...";
            loadForm.Controls.Add(lbl);
            loadForm.Show();
            loadForm.Refresh();
        }
      

        #region GOOGLE_EARTH


        private void deployLocation(List<string> location)
        {
            //name lat long
            //Pullman 46 -117
            string possibles = "";
            while (googleEarth.getFeatures().getFirstChild() != null)
            {
                googleEarth.getFeatures().removeChild(googleEarth.getFeatures().getFirstChild());
            }
            if (googleEarth != null)
            {
                for (int i = 0; i < location.Count; i += 3)
                {
                    string name = location[i];
                    possibles += "\n" + name + ": (" + location[i + 1] + "," + location[i + 2] + ")";
                    /*if (checker.Contains(name))
                    {
                        name = name + int_checker.ToString();
                        int_checker++;
                    }
                    checker.Add(name);
                    double lat = Convert.ToDouble(location[i + 1]);
                    double lon = Convert.ToDouble(location[i + 2]);
                    deployIcon(lat, lon, name, locationID.ToString() + "loc");
                    locationID++;*/
                }
                MessageBox.Show("Possible Locations: \n" + possibles);
            }
        }

        public void JSInitSuccessCallback_(object pluginInstance)
        {
            if (googleEarth == null)
            {
                googleEarth = (IGEPlugin)pluginInstance;
                
                //googleEarthTmp = (IGEPlugin)pluginInstance;
                //setGoogleEarthLocation(46.7325, -117.1717, 350000.0, 450000.0, ++mapID);
                googleEarth.getLayerRoot().enableLayerById(googleEarth.LAYER_BORDERS, 1);
                googleEarth.getNavigationControl().setVisibility(googleEarth.VISIBILITY_SHOW);
                googleEarth.getNavigationControl().setStreetViewEnabled(googleEarth.VISIBILITY_SHOW);
                

                deployIcon(33.4500, -112.0667, "Phoenix", "ID", 0);

            }
        }




        private void setGoogleEarthLocation(double lat, double lon, double alt, double range, double id)
        {
            if (googleEarth != null && this.GEBrowser.Url == new Uri(PLUGIN_URL))
            {
                // Create a new LookAt.
                mapLat = lat;
                mapLong = lon;
                mapAlt = alt;
                mapRange = range;
                mapID = id;
                var lookAt = googleEarth.createCamera(id.ToString());//googleEarth.createLookAt((lat+lon+alt+range).ToString());

                // Set the position values.
                lookAt.setLatitude(lat);//46.7325);
                lookAt.setLongitude(lon);//-117.1717);
                //lookAt.setRange(range);//450000.0); //default is 0.0
                lookAt.setAltitude(alt);//350000.0);

                // Update the view in Google Earth.
                googleEarth.getView().setAbstractView(lookAt);
            }
        }


        private void deployIcon(double lat, double lon, string title, string id, int stars)
        {
            if (googleEarth != null && this.GEBrowser.Url == new Uri(PLUGIN_URL))
            {
               
                // create a point

                KmlPointCoClass point = googleEarth.createPoint(id+"):");
                point.setLatitude(lat);//lookAt.getLatitude());
                point.setLongitude(lon);//lookAt.getLongitude());

                // create a placemark
                KmlPlacemarkCoClass placemark = googleEarth.createPlacemark(id + "placemark");
                placemark.setName(title);

                placemark.setGeometry(point);
                //if (mLink == "") { mLink = "http://www.google.com"; }
                //placemark.setAddress(selectedDes);
                string description = "";
                if (stars > 0)
                {
                    description += "Stars: " + stars.ToString() + "\n";
                }
                description += "http://www.google.com/search?q=yelp+phoenix+az+"+title.Replace(' ', '+');//selectedArt + Environment.NewLine + Environment.NewLine + mLink;
                placemark.setDescription(description);

                icons.Add(placemark);
                var icon = googleEarth.createIcon(id + "icon");
                icon.setHref("http://maps.google.com/mapfiles/kml/paddle/red-circle.png");
                var style = googleEarth.createStyle(id + "style"); //create a new style
                style.getIconStyle().setIcon(icon); //apply the icon to the style
                placemark.setStyleSelector(style); //apply the style to the placemark

                googleEarth.getFeatures().appendChild(placemark);


            }
            setGoogleEarthLocation(lat, lon, 350000.0, 450000.0, ++mapID);

        }


        #endregion


        private void loadComboBoxGiven()
        {
            this.comboBoxGiven.Items.Clear();
            int selected = this.comboBoxQuery.SelectedIndex;

            if (selected == 1 || selected == 2)
            {
                return;
            }

            sqlConnection.Open();
            MySqlCommand cmd = sqlConnection.CreateCommand();

            switch (selected)
            {
                case 0: cmd.CommandText = "select distinct category from businessCategory order by category;";
                    break;
                case 3: cmd.CommandText = "select distinct category from businessCategory order by category;";
                    break;
                case 4: cmd.CommandText = "select distinct category from businessCategory order by category;";
                    break;
                case 5: cmd.CommandText = "select distinct name from userTable order by name;";
                    break;
            }

            MySqlDataReader dataReader = cmd.ExecuteReader();

            while(dataReader.Read())
            {
                this.comboBoxGiven.Items.Add(dataReader[0].ToString());
            }

            sqlConnection.Close();
        }

        private void loadComboBox()
        {
            string[] queryTitles = { 
                                   "1) Given a 'business category' and the current location, find the businesses within 10 miles of the current location. Sort results based on (i) rating (ii) review count (iii) proximity to current location. ",
                                   "2) i) For each business category, find the businesses that were rated best in June 2011. ",
                                   "2) ii) Find the restaurants that steadily improved their ratings during the year of 2012. ",
                                   "3) Given a business category, find the business whose reviews have the most 'Traffic'",
                                   "4) Given business category, create a density map of the highest average stars.",
                                   "5) Given a User Name, find the locations of their reviewed business, and display on map."
                                   };
            for (int i = 0; i < 6; i++)
            {
                this.comboBoxQuery.Items.Add(queryTitles[i]);
            }
        }

        private void loadDataGrid(MySqlDataReader dataReader)
        {            
            dataGridResults.Rows.Clear();
            dataGridResults.Columns.Clear();
            
            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                if (!this.dataGridResults.Columns.Contains(dataReader.GetName(i)))
                {
                    this.dataGridResults.Columns.Add(dataReader.GetName(i), dataReader.GetName(i));

                }
                this.dataGridResults.Columns[i].Frozen = false;
                this.dataGridResults.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                
            }
            int count = 0;
            while (dataReader.Read())
            {
                DataGridViewRow dr = new DataGridViewRow();
                this.dataGridResults.Rows.Add((count + 1).ToString());
                //DataGridViewCheckBoxCell drCheck = (DataGridViewCheckBoxCell)this.dataGridResults.Rows[count].Cells[1];

                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    dataGridResults.Rows[count].Cells[i].Value = dataReader[i].ToString();
                }
             
                count++;
            }
            this.dataGridResults.Rows.Add((count + 1).ToString());
            this.dataGridResults.Rows[count].Cells[0].Value = "Load More...";
        }

        

        private void runQuery(int queryNum)
        {
            createLoadForm();
            sqlConnection.Open();
            MySqlCommand cmd = sqlConnection.CreateCommand();
            string given = "";
            string cmdText = "";
            if (queryNum != 1 && queryNum != 2)
            {
                given = this.comboBoxGiven.SelectedItem.ToString();
            }

            switch (queryNum)
            {
                case 0:
                    //Each degree Latitude = 69 miles 
                    //1 / 69 = 0.01449275362 miles per degree latitude
                    //Phoenix, AZ = (33.4500° N, -112.0667° )
                    //Length of 1 degree of Longitude = cosine (latitude) * length of degree (miles) at equator 
                    //Length(miles) of degree at equator = 69.172
                    //1° Longitude = cos (33.4500) * 69.172 mi = 57.714845217 miles
                    // 1 / 57.714845217 = 0.0173265647
                    double latitude, longitude;
                    Double.TryParse(this.tbxLatitude.Text, out latitude);
                    Double.TryParse(this.tbxLongitude.Text, out longitude);
                    if (latitude == 0 || longitude == 0) { latitude = 33.4500; longitude = -112.0667; }
                    cmdText = "select name, stars as Stars, reviewcount, distance, latitude, longitude from "
        + " (select name, stars, reviewcount, distLat, distLong, sqrt(distLat*distLat + distLong*distLong) as distance, latitude, longitude "
        + " from "//33.4500, -112.0667,
        + " (select name, stars, reviewcount, (latitude -" + latitude.ToString() + ") / 0.01449275362 as distLat, (longitude -" + longitude.ToString() + ") / 0.0173265647 as distLong, latitude, longitude "
        + " from businessTable join businessCategory on (businessTable.business_id = businessCategory.business_id) "
        + " where category = '" + given + "') subQuery1) subQuery2 "
        + " where distance < 10 "
        + " order by stars desc, reviewcount desc, distance "
        + " limit " + maxRows.ToString() + ";";

                    break;
                case 1: cmdText = " select name, category, max(avgStars) as avgStars, latitude, longitude "
        + " from ( "
        + " select name, category, avg(tt1.stars) as avgStars, businessTable.business_id, latitude, longitude "
        + " from (businessTable join "
        + " (select * from businessCategory) tt "
        + " on (tt.business_id = businessTable.business_id)) "
        + " join (select stars, reviewdate, business_id "
        + " from reviewTable "
        + " where reviewdate > '2011-06-01 00:00:00' and reviewdate < '2011-07-01 00:00:00') tt1 "
        + " on (tt1.business_id = businessTable.business_id) "
        + " group by businessTable.business_id) tname "
        + " group by category "
        + " limit " + maxRows.ToString() + ";";

                    break;
                case 2: cmdText = "select q1.name as name, q1.latitude as latitude, q1.longitude as longitude, q1.AvgStars as JanStars, q2.AvgStars as DecStars from "
        + " (select name, latitude, longitude, avg(subQ3.stars) as AvgStars "
        + " from businessTable join (select * from businessCategory where category = 'Restaurants') subQ2 "
        + " on (businessTable.business_id = subQ2.business_id) "
        + " join (select business_id, stars, reviewdate from reviewTable where reviewdate > '2012-01-01 00:00:00' and reviewdate < '2012-02-01 00:00:00') subQ3 "
        + " on (subQ3.business_id = businessTable.business_id) "
        + " group by name) q1 "

        + " join "

        + " (select name, latitude, longitude, avg(subQ3.stars) as AvgStars "
        + " from businessTable join (select * from businessCategory where category = 'Restaurants') subQ2 "
        + " on (businessTable.business_id = subQ2.business_id) "
        + " join (select business_id, stars, reviewdate from reviewTable where reviewdate > '2012-12-01 00:00:00' and reviewdate < '2013-01-01 00:00:00') subQ3 "
        + " on (subQ3.business_id = businessTable.business_id) "
        + " group by name) q2 "

        + " on (q1.name = q2.name) "
        + " where q2.AvgStars > q1.AvgStars "
        + " limit " + maxRows.ToString() + ";";

                    break;
                case 3: cmdText = "select name, sum(reviewTable.stars) as ReviewStars, subQ7.userRate as userRatings, sum(reviewTable.stars) + subQ7.userRate as traffic, latitude, longitude "
        + " from businessTable join "
        + " (select * from businessCategory where category = '" + given + "') subQ6 "
        + " on (subQ6.business_id = businessTable.business_id) "
        + " join reviewTable "
        + " on (reviewTable.business_id = businessTable.business_id) "
        + " join "
        + " (select user_id, votes_funny + votes_useful + votes_cool as userRate "
        + " from userTable) subQ7 "
        + " on (reviewTable.user_id = subQ7.user_id) "
        + " group by businessTable.name "
        + " order by traffic desc "
        + " limit " + maxRows.ToString() + ";";

                    break;
                case 4: cmdText = " select businessTable.name, Stars, latitude, longitude " 
        + " from businessTable join (select * from businessCategory where category = '" + given + "') subQuery9 "
        + " on (businessTable.business_id = subQuery9.business_id) "
	    + " join "
        + " (select max(stars) as maxStars, businessTable.business_id "
        + " from businessTable join businessCategory on (businessTable.business_id = businessCategory.business_id) "
        + " where category = '" + given + "') subQ5 "
        + " where subQ5.maxStars = stars "
        + " order by stars desc "
        + " limit " + maxRows.ToString() + ";";

                    break;
                case 5: cmdText = "select businessTable.name as Name, t4.reviewdate as Date, t4.stars as Stars, t4.text as Text, latitude, longitude " 
        + " from businessTable join "
        + " (select name, reviewdate, business_id, reviewTable.text, stars "
        + " from reviewTable join userTable on (reviewTable.user_id = userTable.user_id) "
        + " where userTable.name = '" + given + "') t4 "
        + " on (t4.business_id = businessTable.business_id) "
        + " order by Stars desc "
        + " limit " + maxRows.ToString() + ";";
                    break;
                
            }

            cmd.CommandText = cmdText;
            MySqlDataReader dataReader = cmd.ExecuteReader();
            
            loadDataGrid(dataReader);
            sqlConnection.Close();

            deployGEIcons();
            
            this.loadForm.Close();
        }
        

        private void clearGEIcons()
        {
            if (this.GEBrowser.Url == new Uri(PLUGIN_URL))
            {
                if (googleEarth != null)
                {
                    while (googleEarth.getFeatures().getFirstChild() != null)
                    {
                        googleEarth.getFeatures().removeChild(googleEarth.getFeatures().getFirstChild());
                    }
                }
            }
        }

        private void deployGEIcons()
        {
            if (this.dataGridResults.Rows != null)
            {

                clearGEIcons();
                for (int i = 0; i < this.dataGridResults.Rows.Count-1; i++)
                {
                    if (this.dataGridResults.Rows[i].Cells["latitude"].Value != null)
                    {
                        double lat, lon;
                        Double.TryParse(this.dataGridResults.Rows[i].Cells["latitude"].Value.ToString(), out lat);
                        Double.TryParse(this.dataGridResults.Rows[i].Cells["longitude"].Value.ToString(), out lon);
                        string name = this.dataGridResults.Rows[i].Cells["name"].Value.ToString();
                        int stars = 0;
                        if (this.dataGridResults.Columns.Contains("Stars")) { Int32.TryParse(this.dataGridResults.Rows[i].Cells["Stars"].Value.ToString(), out stars); }

                        deployIcon(lat, lon, name, (++iconIDCount).ToString(), stars);
                    }
                }
            }

        }

        private void comboBoxQuery_SelectedIndexChanged(object sender, EventArgs e)
        {           
            loadComboBoxGiven();
            int selected = this.comboBoxQuery.SelectedIndex;

            if (previousQuery != selected)
            {
                this.maxRows = 20;
            }

            previousQuery = selected;
            if (selected == 1 || selected == 2)
            {
                runQuery(selected);
            }
            this.GEBrowser.Focus();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            sqlConnection.Close();
        }

        private void comboBoxGiven_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            runQuery(this.comboBoxQuery.SelectedIndex);
            
        
            
            this.GEBrowser.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GEBrowser.Navigate(PLUGIN_URL);
            GEBrowser.ObjectForScripting = this;
            googleEarth = null;
            //deployIcon(33.4500, -112.0667, "Phoenix", "ID");
        }


        private void dataGridResults_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            clearGEIcons();
            for (int i = 0; i < this.dataGridResults.SelectedCells.Count; i++)
            {
                int index = this.dataGridResults.SelectedCells[i].RowIndex;
                if (index >= this.dataGridResults.Rows.Count-2) 
                {
                    if (index == this.dataGridResults.Rows.Count - 2)
                    {
                        maxRows += 20;
                        //runQuery(this.comboBoxQuery.SelectedIndex);
                    }
                    return;
                }


                if (i < this.dataGridResults.Rows.Count)
                {
                    if (this.dataGridResults.Rows[i].Cells["latitude"].Value != null)
                    {
                        double lat, lon;
                        Double.TryParse(this.dataGridResults.Rows[index].Cells["latitude"].Value.ToString(), out lat);
                        Double.TryParse(this.dataGridResults.Rows[index].Cells["longitude"].Value.ToString(), out lon);
                        string name = this.dataGridResults.Rows[index].Cells["name"].Value.ToString();

                        int stars = 0;
                        if (this.dataGridResults.Columns.Contains("Stars"))
                        {
                            if (this.dataGridResults.Rows[index].Cells["Stars"].Value != null)
                            {
                                Int32.TryParse(this.dataGridResults.Rows[index].Cells["Stars"].Value.ToString(), out stars);
                            }
                        }
                        if (index != this.dataGridResults.Columns.Count - 1)
                        {
                            deployIcon(lat, lon, name, (++iconIDCount).ToString(), stars);
                        }


                        if (this.dataGridResults.Columns.Contains("Text"))
                        {
                            int columnIndex = this.dataGridResults.SelectedCells[i].ColumnIndex;
                            if (this.dataGridResults.Columns[columnIndex].Name == "Text")
                            {
                                string text = this.dataGridResults.Rows[index].Cells["Text"].Value.ToString();
                                MessageBox.Show(text);
                                break;
                            }
                        }
                    }
                }
            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            runQuery(this.comboBoxQuery.SelectedIndex);
        }

        
    }
}
