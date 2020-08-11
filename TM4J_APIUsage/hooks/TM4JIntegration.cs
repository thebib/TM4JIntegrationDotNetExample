﻿using Gherkin.Ast;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using TechTalk.SpecFlow;

namespace TM4J_APIUsage.hooks
{
    [Binding]
    public class TM4JIntegration
    {
        public static bool TestCycleCreated;
        public static readonly string JiraProjectKey = "RG";
        public static readonly string JiraAuthToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJkNzNjODMyMy01ZjY1LTMxZjctYjEyMi1kYzYwNzZkYjJmZmYiLCJjb250ZXh0Ijp7ImJhc2VVcmwiOiJodHRwczpcL1wvcm9iZ2Fza2luLWV4YW1wbGUuYXRsYXNzaWFuLm5ldCIsInVzZXIiOnsiYWNjb3VudElkIjoiNWYyOTVhMWY4OTFlZGMwMDIyZmJmZDkzIn19LCJpc3MiOiJjb20ua2Fub2FoLnRlc3QtbWFuYWdlciIsImV4cCI6MTYyODU4NDY0MSwiaWF0IjoxNTk3MDQ4NjQxfQ.4xaTTiTYTwvjltxn1M08tIcn4mANoBxFlHdHJnNzTFU";
        public static string TestCycleKey;

        [BeforeFeature]
        public static void IntegrateWithTM4J()
        {
            if (!TestCycleCreated)
                createTestCycle();
        }

        [AfterScenario]
        public void ScenarioIntegrateWithTM4J(ScenarioContext context)
        {
            updateTestCycle(context);
        }

        /// <summary>
        /// Calls out to TM4J Instance to create a new cycle
        /// </summary>
        private static void createTestCycle()
        { 
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JiraAuthToken);
            var CycleName = $"Automation Run - {DateTime.Now.ToString("dd-mm-yyyy hh:mm:ss")}";
            var jsonData = @$"{{
            ""projectKey"": ""{JiraProjectKey}"",
            ""name"": ""{CycleName}"",
            ""plannedStartDate"": ""{string.Format("{0:yyyy-MM-ddTHH:mm:ss.FFFZ}", DateTime.UtcNow)}""
            }}";
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = client.PostAsync("https://api.adaptavist.io/tm4j/v2/testcycles", stringContent).Result;
            var content = response.Content.ReadAsStringAsync().Result;
            dynamic testCycleConfirmationBody = JsonConvert.DeserializeObject(content);
            TestCycleKey = testCycleConfirmationBody.key;
            TestCycleCreated = true;
        }

        /// <summary>
        /// Calls out to TM4J Instance to append the current test to the created cycle
        /// </summary>
        private static void updateTestCycle(ScenarioContext context)
        {
            var key = context.ScenarioInfo.Tags[0].Split('=')[1];

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JiraAuthToken);

            var result = context.TestError == null ? "Pass" : "Fail";
            var resultComment = context.TestError == null ? "Test Passed" : "Test Failed";
            var jsonData = @$"{{
                    ""projectKey"": ""RG"",
                    ""testCaseKey"": ""{key}"",
                    ""testCycleKey"": ""{TestCycleKey}"",
                    ""statusName"": ""{result}"",
                    ""testScriptResults"": [
                        {{
                            ""statusName"": ""{result}""
                        }}
                    ],
                    ""comment"": ""{resultComment}""
                }}";
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var _ = client.PostAsync("https://api.adaptavist.io/tm4j/v2/testexecutions", stringContent).Result;
        }
    }
}
