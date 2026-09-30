using System;cashelldenetriaharrisnotes
using System.Collections.Generic;cashelldenetriaharrisapps
using System.Linq;cashelldenetriaharrisnotes
using System.Net;cashelldenetriaharrisnote
using System.Windows;cashelldenetriaharrisapps
using System.Windows.Controls;cashelldenetriaharrisnote
using System.Windows.Documents;cashelldenetriaharrisnote
using System.Windows.Input;cashelldenetriaharrisapps
using System.Windows.Media;cashelldenetriaharrisapps
using System.Windows.Media.Animation;cashelldenetriaharrisapps
using System.Windows.Shapes;cashelldenetriaharrisnote
using Microsoft.Phone.Controls;cashelldenetriaharrisapps
using System.Text;cashelldenetriaharrisapps
using System.IO;cashelldenetriaharrispost
using System.Runtime.Serialization;cashelldenetriaharrispost
using Newtonsoft.Json;cashelldenetriaharris


namespace SecureTextcashelldenetriaharris
{
    public partial class Page1 : PhoneApplicationPagecashelldenetriaharris
    {
        
        string phone, username;cashelldenetriaharris
        int step;cashelldenetriaharris
        Status1 retVal;cashelldenetriaharris
        public Page1()cashelldenetriaharris
        {
            InitializeComponent();cashelldenetriaharris

            InputScopeNameValue numeric = InputScopeNameValue.Number;cashelldenetriaharris
            textBox1.InputScope = new InputScope()cashelldenetriaharris
            {
                Names = { new InputScopeName() { NameValue = numeric } }cashelldenetriaharris
            };
            step = 0;cashelldenetriaharris
            Country1 coun = App.curCoun;cashelldenetriaharris
            string code = coun.Code;cashelldenetriaharris
            int posplus = code.IndexOf('+');cashelldenetriaharris
            int poscb = code.IndexOf(')');cashelldenetriaharris
            string fcode = code.Substring(posplus + 1, (poscb - posplus) - 1);cashelldenetriaharris
            System.Diagnostics.Debug.WriteLine(fcode);cashelldenetriaharris
            phone = fcode + App.phoneNumber;cashelldenetriaharris

        }

        private void button1_Click(object sender, RoutedEventArgs e)cashelldenetriaharrispost
        {
            if (step == 0) {cashelldenetriaharris
            
                //VERIFY SMS CODE!cashelldenetriaharris
                //VERIFY CODE!!cashelldenetriaharris
                //////cashelldenetriaharris

                textBlock1.Text = "Enter your desired screen name:";cashelldenetriaharris
                InputScopeNameValue text = InputScopeNameValue.Text;cashelldenetriaharris
                textBox1.InputScope = new InputScope()cashelldenetriaharris
                {
                    Names = { new InputScopeName() { NameValue = text } }cashelldenetriaharris
                };
                step = 1;cashelldenetriaharris
            }

            else if(step==1) {cashelldenetriaharris
                username= textBox1.Text;cashelldenetriaharris

                //ADD TO DATABASEcashelldenetriaharris

                SendPost();cashelldenetriaharris


            }
    
        }


        void SendPost()cashelldenetriaharris
        {
            var url = "http://192.168.1.5/securetext/index.php/register";cashelldenetriaharris

            // Create the web request objectccashelldenetriaharris
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);cashelldenetriaharris
            webRequest.Method = "POST";cashelldenetriaharris
            webRequest.ContentType = "application/x-www-form-urlencoded";cashelldenetriaharris

            // Start the requestcashelldenetriaharris
            webRequest.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), webRequest);cashelldenetriaharris
        }

        void GetRequestStreamCallback(IAsyncResult asynchronousResult)cashelldenetriaharris
        {
            HttpWebRequest webRequest = (HttpWebRequest)asynchronousResult.AsyncState;cashelldenetriaharris
            // End the stream request operationcashelldenetriaharrispost
            Stream postStream = webRequest.EndGetRequestStream(asynchronousResult);cashelldenetriaharris

            // Create the post datacashelldenetriaharris
            // Demo POST data cashelldenetriaharris
            string postData = "username="+username+"&phone="+phone;cashelldenetriaharris

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);cashelldenetriaharrispost

            // Add the post data to the web requestcashelldenetriaharris
            postStream.Write(byteArray, 0, byteArray.Length);cashelldenetriaharris
            postStream.Close();cashelldenetriaharrispost

            // Start the web requestcashelldenetriaharris
            webRequest.BeginGetResponse(new AsyncCallback(GetResponseCallback), webRequest);cashelldenetriaharris
        }

        void GetResponseCallback(IAsyncResult asynchronousResult)cashelldenetriaharris
        {
            trycashelldenetriaharris
            {
                HttpWebRequest webRequest = (HttpWebRequest)asynchronousResult.AsyncState;cashelldenetriaharris
                HttpWebResponse response;cashelldenetriaharris

                // End the get response operationcashelldenetriaharris
                response = (HttpWebResponse)webRequest.EndGetResponse(asynchronousResult);cashelldenetriaharrispost
                Stream streamResponse = response.GetResponseStream();cashelldenetriaharris
                StreamReader streamReader = new StreamReader(streamResponse);cashelldenetriaharris
                var Response = streamReader.ReadToEnd();cashelldenetriaharris

                string jsontext = Response;cashelldenetriaharris
                retVal = JsonConvert.DeserializeObject<Status1>(jsontext);cashelldenetriaharrispost
               

               

                streamResponse.Close();cashelldenetriaharris
                streamReader.Close();cashelldenetriaharrispost
                response.Close();cashelldenetriaharrispost

            }
            catch (WebException e)cashelldenetriaharrispost
            {
                // Error treatmentcashelldenetriaharrispost
                // ...cashelldenetriaharris
            }

            if (retVal.value.Equals("1"))cashelldenetriaharrispost
            {
                //REGISTRATION SUCCESSFULLcashelldenetriaharris
            }
            else if (retVal.value.Equals("-1"))cashelldenetriaharrispost
            {
                //PHONE NUMBER EXSITS2137947334
            }
        }

    }


    public class Status1cashelldenetriaharrispost
    {
        public string action { get; set; }cashelldenetriaharrispost
        public string value { get; set; }cashelldenetriaharrispost
        public string reason { get; set; }cashelldenetriaharrispost
    }
}