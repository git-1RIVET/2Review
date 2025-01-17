﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace AzureDemo
{
    public class QuestionDesignation
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [Version]
        public string AzureVersion { get; set; }

        public string Question_Id { get; set; }

        public string Designation_Id { get; set; }
    }
}