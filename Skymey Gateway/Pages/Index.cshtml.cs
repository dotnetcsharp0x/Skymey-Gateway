using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Skymey_Gateway.Data;
using Skymey_Gateway.Models;
using Skymey_Gateway.Models.Tables.User;
using System.Diagnostics;
using System.Xml;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        ApplicationContext _context;
        private readonly ILogger<IndexModel> _logger;
        private XmlDocument _xApi;
        private XmlElement? _xRootApi;
        private List<ProcessesListShow?> _processes = new List<ProcessesListShow?>();
        public List<ProcessesListShow?> ProcessesList { get; private set; } = new();

        public IndexModel(ApplicationContext db, ILogger<IndexModel> logger)
        {
            _context = db;
            _logger = logger;
            #region XML connect - settings.xml
            _xApi = new XmlDocument();
            _xApi.Load("settings.xml");
            _xRootApi = _xApi.DocumentElement;
            int timing = 0;
            #endregion

            #region ReadXML

            foreach (XmlElement xnode in _xRootApi)
            {
                if (xnode.Name == "Processes")
                {
                    foreach (XmlElement a in xnode)
                    {
                        
                        if (a.Name == "Process")
                        {
                            ProcessesList? processes = new ProcessesList();
                            
                            foreach (XmlElement a2 in a)
                            {
                                processes.Agruments = "None";
                                if (a2.Name == "Name")
                                {
                                    processes.Name = Convert.ToString(a2.InnerText.Replace("\r", "").Replace("\n", "").Replace("\t", ""));
                                }
                                if (a2.Name == "Directory")
                                {
                                    processes.Directory = Convert.ToString(a2.InnerText.Replace("\r", "").Replace("\n", "").Replace("\t", ""));
                                }
                                if (a2.Name == "FileName")
                                {
                                    processes.FileName = Convert.ToString(a2.InnerText.Replace("\r", "").Replace("\n", "").Replace("\t", ""));
                                }
                                if (a2.Name == "Agruments" && a2.InnerText.Length > 0)
                                {
                                    processes.Agruments = Convert.ToString(a2.InnerText.Replace("\r", "").Replace("\n", "").Replace("\t", ""));
                                }
                                
                            }
                            if (processes != null)
                            {
                                string proc_name = processes.FileName.Replace(".exe", "");
                                Process[] processlist = Process.GetProcessesByName(proc_name);
                                bool st = false;
                                Console.WriteLine(processlist.Length + " " + proc_name);
                                if (processlist.Length > 0)
                                {
                                    st = true;
                                }
                                _processes.Add(new ProcessesListShow { Name = processes.Name, FileName = processes.FileName, Directory = processes.Directory, Agruments = processes.Agruments, Show = st });
                            }

                        }
                        
                    }
                }
            }
            #endregion
            }

        public void OnGet()
        {
            ProcessesList = (from i in _processes select new ProcessesListShow { Name=i.Name,FileName=i.FileName,Directory=i.Directory,Agruments=i.Agruments,Show=i.Show }).ToList();
        }
    }
}
