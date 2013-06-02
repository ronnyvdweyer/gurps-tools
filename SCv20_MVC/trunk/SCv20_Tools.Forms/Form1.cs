using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SCv20_Tools.Core.Services;


using Microsoft.Reporting.WinForms;
using System.Reflection;
using System.IO;
using SCv20_Tools.Core.Domain;
namespace SCv20_Tools.Forms {
    public partial class Form1 : Form {

        private readonly DataService _dataService;

        public Form1() {
            InitializeComponent();
            _dataService = DataService.GetInstance();
        }

        private void Form1_Load(object sender, EventArgs e) {
           
        }

        private void btnMission_Click(object sender, EventArgs e) {
            var ds = _dataService.GetMission(1);// .GetAllQualities(null);

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.Reset();
            reportViewer.ProcessingMode = ProcessingMode.Local;


            Assembly assembly = Assembly.LoadFrom("SCv20_Tools.Core.dll");
            Stream stream = assembly.GetManifestResourceStream("SCv20_Tools.Core.Reports.Mission.rdlc");
            reportViewer.LocalReport.LoadReportDefinition(stream);
            //reportViewer.LocalReport.ReportEmbeddedResource = "SCv20_Tools.Core.Sheets.Mission.rdlc, SCv20_Tools.Core";


            reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer.ZoomMode = ZoomMode.Percent;
            reportViewer.ZoomPercent = 150;


            reportViewer.LocalReport.Refresh();
            reportViewer.Refresh();

            var qualities = ds.Qualities.Select(rows => rows.Quality).ToList();
            var missions = new List<Mission>();
            missions.Add(ds);

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("dsQualities", qualities));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("dsMission", missions));

            reportViewer.Dock = DockStyle.Fill;
            pnlReport.Controls.Add(reportViewer);

            reportViewer.RefreshReport();
        }
    }
}
