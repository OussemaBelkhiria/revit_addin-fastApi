using Autodesk.Revit.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Printing;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace RevitAddin
{
    /// <summary>
    /// Interaction logic for HomeworkUI.xaml
    /// </summary>
    public partial class HomeworkUI : UserControl
    {
        public HomeworkUI()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // read the input from the text :
            SendBtn.IsEnabled = false;

            String text = RequestTextBox.Text;
            ResponseTextBox.Text = "";

                try
                {
                    JObject obj = JObject.Parse(text);

                    if (obj.Properties().Count() > 1 || (obj.Properties().Count() == 1 && obj.Property("msg") == null))
                    {
                        TaskDialog.Show("Error :", "please use the specified structure in the example");
                        SendBtn.IsEnabled = true;
                        return;
                    }
                    if (((string?)obj.Property("msg").Value) == "") {
                        TaskDialog.Show("Error :", "please write some text");
                        SendBtn.IsEnabled = true;
                        return;
                    }

                        using (HttpClient client = new HttpClient())
                    {
                        try
                        {

                            
                            string url = "http://localhost:8000/"; 
                            var content = new StringContent(text, Encoding.UTF8, "application/json");

                            HttpResponseMessage response = await client.PostAsync(url, content);

                            response.EnsureSuccessStatusCode();
                            
                            string responseText = await response.Content.ReadAsStringAsync();
                       

                            // formatting
                            JObject parsedResponse = JObject.Parse(responseText);
                            string formattedJson = JsonConvert.SerializeObject(parsedResponse, Newtonsoft.Json.Formatting.Indented);
                            ResponseTextBox.Text = formattedJson;

                       
                            
                            SendBtn.IsEnabled = true;

                          
                        }
                        catch { TaskDialog.Show("Error", "Server is not running");
                                SendBtn.IsEnabled = true;
                        }

                       
                    }
                }
                catch (JsonReaderException ex) {
                    TaskDialog.Show("Error:", "Not a correct JSON format");
                    SendBtn.IsEnabled = true;
                }
               
               

        }
        public string RequestSampleText { get; set; } =
@"{
    ""msg"": ""write your input here""
}";

    }
}
