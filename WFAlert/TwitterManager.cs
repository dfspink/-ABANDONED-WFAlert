using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using LinqToTwitter;

namespace WFAlert
{
    class TwitterManager
    {
        public string code="abc";
        public int needcode = 0;
        public static TwitterContext twitterCtx;

        const string CredentialsFile = "credentials.txt";

        public void Run()
        {
            ITwitterAuthorizer auth = PerformAuthorization();

            if (auth == null)
            {
                MessageBox.Show("Authorization Failed");
                return;
            }

            twitterCtx = new TwitterContext(auth);
        }

        private ITwitterAuthorizer PerformAuthorization()
        {
            InMemoryCredentials credentials;

            PermGUI GUI3 = new PermGUI();
            Application.Run(GUI3);

            // validate that credentials are present
            if (!GetCredentials(out credentials, needcode))
            {
                return null;
            }

            // configure the OAuth object
            var auth = new PinAuthorizer
            {
                Credentials = credentials,
                UseCompression = true,
                GoToTwitterAuthorization = pageLink => Process.Start(pageLink),

                GetPin = () =>
                {
                    // this executes after user authorizes, which begins with the call to auth.Authorize() below.
                    AuthGUI GUI2 = new AuthGUI();
                    Application.Run(GUI2);
                    return code;
                }
            };

            // start the authorization process (launches Twitter authorization page).
            try
            {
                auth.Authorize();
            }
            catch (WebException wex)
            {
                MessageBox.Show("Webexeption says: \n" + wex.Message);
            }

            File.WriteAllLines(CredentialsFile, new string[] { auth.Credentials.ToString() });

            return auth;
        }

        static bool GetCredentials(out InMemoryCredentials credentials, int reset)
        {
            credentials = new InMemoryCredentials();

            if (reset == 0)
            {
                if (File.Exists(CredentialsFile))
                {
                    string[] lines = File.ReadAllLines(CredentialsFile);
                    if (lines != null && lines.Length > 0)
                    {
                        credentials.Load(lines[0]);
                        return true;
                    }
                }

                // validate that credentials are present
                if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["twitterConsumerKey"]) ||
                    string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["twitterConsumerSecret"]))
                {
                    MessageBox.Show("You need to set twitterConsumerKey and twitterConsumerSecret \n" +
                                        "in App.config/appSettings.");

                    return false;
                }

                credentials = new InMemoryCredentials
                {
                    ConsumerKey = ConfigurationManager.AppSettings["twitterConsumerKey"],
                    ConsumerSecret = ConfigurationManager.AppSettings["twitterConsumerSecret"]
                };

                return true;
            }
            else
            {
                credentials = new InMemoryCredentials
                {
                    ConsumerKey = ConfigurationManager.AppSettings["twitterConsumerKey"],
                    ConsumerSecret = ConfigurationManager.AppSettings["twitterConsumerSecret"]
                };

                return true;
            }
        }

        public void SetCode(string input)
        {
            code = input;
        }

        public void SetNeedCode(int input)
        {
            needcode = input;
        }

        public TwitterContext GetCtx()
        {
            return twitterCtx;
        }
    }
}