using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;

namespace WinFormHubTest
{
    public partial class Form1 : Form
    {
        public string UrlHubAPI { get; private set; }

        public Form1()
        {
            InitializeComponent();
            UrlHubAPI = "https://inaipisrhubs.azurewebsites.net/ApoyoAlimentosHub";

            Init();
        }

        private async void Init()
        {
            _ = await GetSimpleHubsInstance();
            AddButton();

            SimpleHubConn.On<string>("RefreshDashboardRequest", (fecha) =>
            {
                MessageBox.Show("Rasengan, mató " + fecha);
            });
        }

        private void AddButton()
        {
            Button btn = new Button();
            btn.Name = nameof(btn);
            btn.Click += Btn_Click;
            btn.Text = "Si me quieres comer, sigeme mi amor";

            this.Controls.Add(btn);
        }

        private async void Btn_Click(object sender, EventArgs e)
        {
            await SimpleHubConn.InvokeAsync("RefreshDashboards");
        }

        private HubConnection SimpleHubConn;

        public async Task<HubConnection> GetSimpleHubsInstance()
        {
            if (SimpleHubConn == null || SimpleHubConn.State == HubConnectionState.Disconnected)
            {
                SimpleHubConn = new HubConnectionBuilder().WithUrl(UrlHubAPI).Build();
                await SimpleHubConn.StartAsync();
            }

            return SimpleHubConn;
        }
    }
}
