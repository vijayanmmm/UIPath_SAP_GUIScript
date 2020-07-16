using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPFEWSELib;
using System.Runtime.InteropServices;
using SapROTWr;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using System.Diagnostics;


namespace VJ.SAPGUIScript.Activities
{
    public class SAPAuto
    {
        public GuiSession getCurrentSession(String MainWindowName = "")
        {
            ////GuiApplication appSAP = new GuiApplication();
            //GuiApplication appSAP = (GuiApplication)Marshal.GetActiveObject("SAPGUISERVER");
            //return appSAP.ActiveSession;
            String strLog;
            object rot;
            object engine;
            CSapROTWrapper sapROTWrapper;
            strLog = "GOING TO GET SAPGUI";
            try
            {
                sapROTWrapper = new CSapROTWrapper();
                rot = sapROTWrapper.GetROTEntry("SAPGUI");
                strLog = strLog + Environment.NewLine + "GOING TO GET SCRIPT ENGINE";
                engine = rot.GetType().InvokeMember("GetScriptingEngine", System.Reflection.BindingFlags.InvokeMethod, null, rot, null);
            }
            catch (Exception e)
            {
                throw new Exception(strLog + Environment.NewLine + e.Message + e.StackTrace); throw;
            }
            GuiConnection SapconnSession;
            //validate whether GuiApplication has chidren or not "Engine"

        
            if (engine is null)
            {
                throw new Exception("SAP GUI Application is null");
            }
            else
            {
                strLog = strLog + Environment.NewLine + "SAP GUI Application is not null";
                if ( (engine as GuiApplication).Children is null)
                {
                    strLog = strLog + Environment.NewLine + "SAP GUI Application children GUI Application is null";
                }
                else
                {
                    strLog = strLog + Environment.NewLine + "SAP GUI Application has :" + (engine as GuiApplication).Children.Count + " children";
                }
            }

            try
            {
                SapconnSession = (engine as GuiApplication).Children.Item(0) as GuiConnection;
            }
            catch (Exception)
            {
                try
                {
                    SapconnSession = (engine as GuiApplication).Children.Item(1) as GuiConnection;
                }
                catch (Exception e)
                {

                    throw new Exception(strLog + Environment.NewLine + e.Message + e.StackTrace);
                }               
            }


            GuiSession SapSession = null;
            strLog = strLog + Environment.NewLine + "Get SAP Connection session";

            try
            {
                strLog = strLog + Environment.NewLine + "SAP Connection Session has :" + SapconnSession.Children + " children";
          
            if (MainWindowName != "")
            {
                for (int i = 0; i < SapconnSession.Children.Count; i++)
                {
                    string strWindowName = ((SapconnSession.Children.Item(i) as GuiSession).Children.Item(0) as GuiMainWindow).Text;
                    //Debug.Print("Main Window Name:" +  strWindowName);
                    if (strWindowName == MainWindowName)
                    {
                        SapSession = SapconnSession.Children.Item(i) as GuiSession;
                        break;
                    }
                    //Debug.Print((SapconnSession.Children.Item(i) as GuiSession).ActiveWindow.Text);
                }
                if (SapSession == null)
                {
                    throw new Exception("Could not find the window name in the available session");
                }
            }
            else
            {
                try
                {
                    strLog = strLog + Environment.NewLine + "Going to get First children as GuiSession";
                    SapSession = SapconnSession.Children.Item(0) as GuiSession;
                }
                catch (Exception)
                {
                    strLog = strLog + Environment.NewLine + "Going to get Second children as GuiSession since first failed";
                    SapSession = SapconnSession.Children.Item(1) as GuiSession;
                }
                
            }
            }
            catch (Exception e)
            {
                throw new Exception(strLog + Environment.NewLine + e.Message + e.StackTrace);               
            }

            //GuiSession SapSession = (rot as GuiApplication).Connections.ElementAt(0) as GuiSession;
            return SapSession;
        }

        /// <summary>
        /// This function opens the new SAP session
        /// </summary>
        /// <param name="env">Provide the available environment name, for example 'NRP'</param>
        /// <returns></returns>
        public GuiSession openSAP(string env)
        {
            GuiApplication appSAP = new GuiApplication();
            GuiConnection connSAP;
            string connectString = null;
            if (env.ToUpper().Equals("DEFAULT"))
            {
                connectString = "1.0 Test ERP (DEFAULT)";
            }
            else
            {
                connectString = env;
            }
            connSAP = appSAP.OpenConnection(connectString, Sync: true); //creates connection
            var output = appSAP.ActiveSession;

            return (GuiSession)connSAP.Sessions.Item(0); //creates the Gui session off the connection you made
        }

        /// <summary>
        /// This function will login into the SAP with provided credentials(same as manual login)
        /// </summary>
        /// <param name="SapSession"></param>
        /// <param name="myclient"></param>
        /// <param name="mylogin"></param>  
        /// <param name="mypass"></param>
        /// <param name="mylang"></param>
        public string login(string servername, string myclient, string mylogin, System.Security.SecureString mypass, string mylang)
        {
            //GuiApplication appSAP = (GuiApplication)Marshal.GetActiveObject("SAPGUI");
            //GuiConnection connSAP = appSAP.OpenConnection(server, Sync: true);
            //GuiSession SapSession = (GuiSession)connSAP.Sessions.Item(0);

            //    System.IO.StreamWriter m_fswLogFile;
            //DateTime m_dtLastTimeLog = DateTime.MinValue;
            //int m_intTimeLogInterVal;

            //String strFilePath = "D:\\AA_Log_SAPAUto_Metabot.txt";

            //if (!System.IO.File.Exists(strFilePath)) {
            //    System.IO.File.Create(strFilePath).Close();
            //}
            ////Open the log file
            //m_fswLogFile = new System.IO.StreamWriter(strFilePath, true);
            //m_fswLogFile.AutoFlush = true;
            //m_fswLogFile.WriteLine("Parameters");
            //m_fswLogFile.WriteLine(server);
            //m_fswLogFile.WriteLine(mylogin);
            //m_fswLogFile.WriteLine(mypass);
            //m_fswLogFile.WriteLine(mylang);
            //m_fswLogFile.WriteLine(SAPWindowName);
            //m_fswLogFile.Close();
            //m_fswLogFile = null;

            //Close the SAP if its opened, then open the new process
            KillProcess("saplogon");
       
            //Set the Exe Path and SAP Window name
            string strSAPLoginPadPath = @"C:\Program Files (x86)\SAP\FrontEnd\SapGui\saplogon.exe";
            FileVersionInfo fi2 = FileVersionInfo.GetVersionInfo(strSAPLoginPadPath);
            string strVersion = fi2.ProductVersion.Substring(0, 3);
            String strloginWindowName = "SAP Logon " + strVersion;

            //Open the SAP Logon Window
            StartProcess(strSAPLoginPadPath);

            //Wait for the window to be loaded.
            System.Threading.Thread.Sleep(1000);
            AutomationElement desktop = AutomationElement.RootElement;
            AutomationElement ele = null;
            while (ele is null)
            {
                try
                {
                    ele = desktop.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, strloginWindowName));
                }
                catch { }
            }

            CSapROTWrapper sapROTWrapper = new CSapROTWrapper();
            object rot = sapROTWrapper.GetROTEntry("SAPGUI");
            object engine = rot.GetType().InvokeMember("GetScriptingEngine", System.Reflection.BindingFlags.InvokeMethod, null, rot, null);
            GuiConnection connection = (engine as GuiApplication).OpenConnection(servername);
            GuiSession SapSession = connection.Children.ElementAt(0) as GuiSession;

            //m_fswLogFile = new System.IO.StreamWriter(strFilePath, true);
            //m_fswLogFile.AutoFlush = true;
            //m_fswLogFile.WriteLine("Going to get the controls for login");

            //m_fswLogFile.Close();
            //m_fswLogFile = null;

            GuiTextField client = (GuiTextField)SapSession.ActiveWindow.FindByName("RSYST-MANDT", "GuiTextField");
            GuiTextField login = (GuiTextField)SapSession.ActiveWindow.FindByName("RSYST-BNAME", "GuiTextField");
            GuiTextField pass = (GuiTextField)SapSession.ActiveWindow.FindByName("RSYST-BCODE", "GuiPasswordField");
            GuiTextField language = (GuiTextField)SapSession.ActiveWindow.FindByName("RSYST-LANGU", "GuiTextField");
            //m_fswLogFile = new System.IO.StreamWriter(strFilePath, true);
            //m_fswLogFile.AutoFlush = true;
            //m_fswLogFile.WriteLine("Enter credentials for login");

            //m_fswLogFile.Close();
            //m_fswLogFile = null;
            client.SetFocus();
            client.Text = myclient;
            login.SetFocus();
            login.Text = mylogin;
            pass.SetFocus();
            pass.Text = new System.Net.NetworkCredential(string.Empty, mypass).Password;
            language.SetFocus();
            language.Text = mylang;

            //m_fswLogFile = new System.IO.StreamWriter(strFilePath, true);
            //m_fswLogFile.AutoFlush = true;
            //m_fswLogFile.WriteLine("click login");

            //m_fswLogFile.Close();
            //m_fswLogFile = null;
            //Press the green checkmark button which is about the same as the enter key 
            GuiButton btn = (GuiButton)SapSession.FindById("/app/con[0]/ses[0]/wnd[0]/tbar[0]/btn[0]");
            btn.SetFocus();
            btn.Press();

            //Validate whether the login successfull or Not
            //If the error is "Already logged in by the same user, then click            
            GuiStatusbar statusBar = (GuiStatusbar)SapSession.FindById("wnd[0]/sbar");
            string strMsg = statusBar.Text;
            string strMsgType = statusBar.MessageType;
            if (strMsg.Contains("User already logged on at another terminal") == true || strMsg.Contains("用户已经登录到其它终端") == true)//(strMsg == "W" || strMsgType == "E" ) //'Warning or Error
            {
                //Check whether is it showing the same terminal or different terminal
                string strWinText = ((GuiFrameWindow)SapSession.FindById("wnd[1]")).Text;
                strWinText = Environment.MachineName;
                if (strWinText.Contains(Environment.MachineName) == true)
                {
                    //This represents, the login in the same machine, then click continue with current login option
                    ((GuiRadioButton)SapSession.FindById("wnd[1]/usr/radMULTI_LOGON_OPT1")).Select();
                    ((GuiButton)SapSession.FindById("wnd[1]/tbar[0]/btn[0]")).Press();
                }
                else
                {
                    SapSession.ActiveWindow.Close();
                    return "SAP Error while logging in the SAP system, Error:\n" + strMsg;
                }
            }
            else if (strMsg.Length > 0 && strMsgType == "E")
            {
                SapSession.ActiveWindow.Close();
                return "SAP Error while logging in the SAP system, Error:\n" + strMsg;
            }

            return "success";
        }

        public string login_SSO(string servername)
        {         
            //Close the SAP if its opened, then open the new process
            KillProcess("saplogon");

            //Set the Exe Path and SAP Window name
            string strSAPLoginPadPath = @"C:\Program Files (x86)\SAP\FrontEnd\SapGui\saplogon.exe";
            FileVersionInfo fi2 = FileVersionInfo.GetVersionInfo(strSAPLoginPadPath);
            string strVersion = fi2.ProductVersion.Substring(0, 3);
            String strloginWindowName = "SAP Logon " + strVersion;

            //Start SAP Login Window
            StartProcess(strSAPLoginPadPath);  
            
            //Wait for the window to be loaded.
            System.Threading.Thread.Sleep(1000);
            AutomationElement desktop = AutomationElement.RootElement;
            AutomationElement ele = null;
            while (ele is null)
            {
                try
                {
                    ele = desktop.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, strloginWindowName));
                }
                catch { }
            }

            CSapROTWrapper sapROTWrapper = new CSapROTWrapper();
            object rot = sapROTWrapper.GetROTEntry("SAPGUI");
            object engine = rot.GetType().InvokeMember("GetScriptingEngine", System.Reflection.BindingFlags.InvokeMethod, null, rot, null);
            GuiConnection connection = (engine as GuiApplication).OpenConnection(servername);
            GuiSession SapSession = connection.Children.ElementAt(0) as GuiSession;

            //Validate whether the login successfull or Not
            //If the error is "Already logged in by the same user, then click            
            GuiStatusbar statusBar = (GuiStatusbar)SapSession.FindById("wnd[0]/sbar");
            string strMsg = statusBar.Text;
            string strMsgType = statusBar.MessageType;
            if (strMsg.Contains("User already logged on at another terminal") == true || strMsg.Contains("用户已经登录到其它终端") == true)//(strMsg == "W" || strMsgType == "E" ) //'Warning or Error
            {
                //Check whether is it showing the same terminal or different terminal
                string strWinText = ((GuiFrameWindow)SapSession.FindById("wnd[1]")).Text;
                strWinText = Environment.MachineName;
                if (strWinText.Contains(Environment.MachineName) == true)
                {
                    //This represents, the login in the same machine, then click continue with current login option
                    ((GuiRadioButton)SapSession.FindById("wnd[1]/usr/radMULTI_LOGON_OPT1")).Select();
                    ((GuiButton)SapSession.FindById("wnd[1]/tbar[0]/btn[0]")).Press();
                }
                else
                {
                    //SapSession.ActiveWindow.Close();
                    throw new Exception( "SAP Error while logging in the SAP system, Error:\n" + strMsg);
                }
            }
            else if (strMsg.Length > 0 && strMsgType == "E")
            {
                //SapSession.ActiveWindow.Close();
                throw new Exception("SAP Error while logging in the SAP system, Error:\n" + strMsg);
            }

            return "success";
        }


        public bool KillProcess(string strProcessName)
        {
            try
            {
                Process[] myproc = Process.GetProcesses();
                foreach (Process item in myproc)
                {
                    if (item.ProcessName == strProcessName)
                    {
                        item.Kill();
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool StartProcess(string strProcessName)
        {
            Process pro = Process.Start(strProcessName);
            return true;
        }

        public void TCODEInput(string TCODE)
        {
            GuiSession SapSession = getCurrentSession();
            SapSession.StartTransaction(TCODE);
        }

        public Boolean SAPBusy(string MainWindowName)
        {
            GuiSession SapSession = getCurrentSession(MainWindowName);
            return SapSession.Busy;
        }

        public bool logoff()
        {
            GuiSession SapSession = getCurrentSession();
            //Log Off the SAP
            SapSession.ActiveWindow.Close();
            GuiButton btnLogOffOk = (GuiButton)SapSession.FindById("/app/con[0]/ses[0]/wnd[1]/usr/btnSPOP-OPTION1");
            btnLogOffOk.SetFocus();
            btnLogOffOk.Press();

            return true;
        }

        private bool TextInput(string strControlID, string strValue)
        {
            GuiSession SapSession = getCurrentSession();
            GuiTextField txtControl = (GuiTextField)SapSession.ActiveWindow.FindByName(strControlID, "GuiTextField");
            txtControl.SetFocus();
            txtControl.Text = strValue;
            return true;
        }

        public bool TextInputByID(string strControlID, string strValue)
        {
            GuiSession SapSession = getCurrentSession();
            GuiTextField txtControl = (GuiTextField)SapSession.ActiveWindow.FindById(strControlID, "GuiTextField");
            txtControl.SetFocus();
            txtControl.Text = strValue;
            return true;
        }

        public string Get_TextBox_Value_ByID(string strControlID)
        {
            GuiSession SapSession = getCurrentSession();
            //GuiTextField txtControl = (GuiTextField)SapSession.ActiveWindow.FindById(strControlID, "GuiTextField");
            GuiTextField txtControl = (GuiTextField)SapSession.ActiveWindow.FindById(strControlID, "GuiTextField");
            return txtControl.Text;
        }

        public string Get_TextEditBox_Value_ByID(string strControlID)
        {
            GuiSession SapSession = getCurrentSession();
            //GuiTextField txtControl = (GuiTextField)SapSession.ActiveWindow.FindById(strControlID, "GuiTextField");
            GuiTextedit txtControl = (GuiTextedit)SapSession.ActiveWindow.FindById(strControlID, "GuiTextedit");
            return txtControl.Text;
        }


        public bool CheckBoxSelectByID(string strControlID, string strValue)
        {
            GuiSession SapSession = getCurrentSession();
            GuiCheckBox chkControl = (GuiCheckBox)SapSession.ActiveWindow.FindById(strControlID, "GuiTextField");
            chkControl.SetFocus();
            if (strValue.ToString().ToLower().Trim() == "false")
            {
                chkControl.Selected = false;
            }
            else
            {
                chkControl.Selected = true;
            }

            return true;
        }

        public bool MenuSelectionByID(string strMenuID)
        {
            GuiSession SapSession = getCurrentSession();
            GuiMenu menu = (GuiMenu)SapSession.ActiveWindow.FindById(strMenuID, "GuiMenu");
            menu.Select();
            return true;
        }

        public bool TableRow_Select(string strControlID, string RowIndex)
        {
            int intRowIndex = Convert.ToInt32(RowIndex);
            GuiSession SapSession = getCurrentSession();
            GuiTableControl tblControl = (GuiTableControl)SapSession.ActiveWindow.FindById(strControlID, "GuiTableControl");
            tblControl.SetFocus();
            tblControl.GetAbsoluteRow(intRowIndex).Selected = true;
            return true;
        }

        public bool TreeView_Select(string strControlID, string strKey)
        {
            GuiSession SapSession = getCurrentSession();
            GuiTree treeControl = (GuiTree)SapSession.ActiveWindow.FindById(strControlID, "GuiTree");
            treeControl.SetFocus();
            treeControl.SelectNode(strKey);
            return true;
        }

        public bool TreeItem_DoubleClick(string strControlID, string strKey)
        {
            GuiSession SapSession = getCurrentSession();
            GuiTree treeControl = (GuiTree)SapSession.ActiveWindow.FindById(strControlID, "GuiTree");
            treeControl.SetFocus();
            treeControl.SelectNode(strKey);
            treeControl.DoubleClickNode(strKey);
            return true;
        }

        public bool RadioBoxSelectByID(string strControlID)
        {
            GuiSession SapSession = getCurrentSession();
            GuiRadioButton rdoControl = (GuiRadioButton)SapSession.ActiveWindow.FindById(strControlID, "GuiTextField");
            rdoControl.SetFocus();
            rdoControl.Select();
            return true;
        }

        public string GetWindowText(string strWindowID)
        {
            GuiSession SapSession = getCurrentSession();
            var wndWindow = SapSession.ActiveWindow.FindById(strWindowID, "GuiMainWindow");
            if (wndWindow.Type == "GuiMessageWindow")
            {
                GuiMessageWindow wndMsgWindow = (GuiMessageWindow)wndWindow;
                return wndMsgWindow.MessageText;
            }
            else if (wndWindow.Type == "GuiFrameWindow")
            {
                GuiFrameWindow wndFrmWindow = (GuiFrameWindow)wndWindow;
                return wndFrmWindow.AccText;
            }
            else if (wndWindow.Type == "GuiMainWindow")
            {
                GuiMainWindow wndMainWindow = (GuiMainWindow)wndWindow;
                return wndMainWindow.AccText;
            }
            else if (wndWindow.Type == "GuiModalWindow")
            {
                GuiModalWindow wndMainWindow = (GuiModalWindow)wndWindow;
                return wndMainWindow.PopupDialogText;
            }
            else
            {
                return "";
            }
        }

        //public string GetMsgWindowText(string strWindowID) {
        //    GuiSession SapSession = getCurrentSession();
        //    GuiMessageWindow wndMsgWindow = (GuiMessageWindow)SapSession.ActiveWindow.FindById(strWindowID, "GuiMessageWindow");
        //    wndMsgWindow.SetFocus();
        //    return wndMsgWindow.Text;
        //}

        //public string GetFrameWindowText(string strWindowID) {
        //    GuiSession SapSession = getCurrentSession();
        //    GuiFrameWindow wndFrmWindow = (GuiFrameWindow)SapSession.ActiveWindow.FindById(strWindowID, "GuiFrameWindow");
        //    wndFrmWindow.SetFocus();
        //    return wndFrmWindow.Text;
        //}

        //public string GetMainWindowText(string strWindowID) {
        //    GuiSession SapSession = getCurrentSession();
        //    GuiMainWindow wndMainWindow = (GuiMainWindow)SapSession.ActiveWindow.FindById(strWindowID, "GuiMainWindow");
        //    wndMainWindow.SetFocus();
        //    return wndMainWindow.Text;
        //}

        public void ContextMenuSelectItem(string GridID, string ContextMenuItem)
        {
            if (GridID == "") GridID = "wnd[0]/usr/cntlGRID1/shellcont/shell";
            GuiSession SapSession = getCurrentSession();
            GuiGridView gridView = (GuiGridView)SapSession.ActiveWindow.FindById(GridID, "GuiGridView");
            gridView.ContextMenu();
            gridView.SelectContextMenuItemByText(ContextMenuItem);
        }

        public void GridSelectAll(string GridID)
        {
            GuiSession SapSession = getCurrentSession();
            GuiGridView gridView = (GuiGridView)SapSession.ActiveWindow.FindById(GridID, "GuiGridView");
            gridView.SelectAll();
        }

        public string Grid_CountRows(string GridID)
        {
            GuiSession SapSession = getCurrentSession();
            GuiGridView gridView = (GuiGridView)SapSession.ActiveWindow.FindById(GridID, "GuiGridView");
            return gridView.RowCount.ToString();
        }

        public void Grid_SelectRow_AndDoubleClick(string GridID, string rowIndex)
        {
            GuiSession SapSession = getCurrentSession();
            GuiGridView gridView = (GuiGridView)SapSession.ActiveWindow.FindById(GridID, "GuiGridView");
            gridView.SelectedRows = rowIndex;
            gridView.DoubleClickCurrentCell();
        }

        public void Grid_SelectCell_AndDoubleClick(string GridID, string rowIndex, string columnID)
        {
            GuiSession SapSession = getCurrentSession();
            GuiGridView gridView = (GuiGridView)SapSession.ActiveWindow.FindById(GridID, "GuiGridView");
            gridView.SetCurrentCell(Convert.ToInt32(rowIndex), columnID);
            gridView.DoubleClickCurrentCell();
        }
        public string Grid_GetCellValue(string GridID, Int32 rowIndex, string columnID)
        {
            GuiSession SapSession = getCurrentSession();
            GuiGridView gridView = (GuiGridView)SapSession.ActiveWindow.FindById(GridID, "GuiGridView");
            return gridView.GetCellValue(rowIndex, columnID);
        }

        public string Table_GetCellValue(string TableID, int rowIndex, int columnIndex)
        {       
            GuiSession SapSession = getCurrentSession();
            GuiTableControl tblControl = (GuiTableControl)SapSession.ActiveWindow.FindById(TableID, "GuiTableControl");
            tblControl.SetFocus();
            return tblControl.GetCell(Convert.ToInt32(rowIndex), Convert.ToInt32(columnIndex)).Text;    
        }

        public void Grid_Select_Row(string GridID, string rowIndex)
        {
            GuiSession SapSession = getCurrentSession();
            GuiGridView gridView = (GuiGridView)SapSession.ActiveWindow.FindById(GridID, "GuiGridView");
            gridView.SelectedRows = rowIndex;
        }

        public bool Grid_ToolBarButton_PressByName(string GridID, string ButtonName)
        {
            GuiSession SapSession = getCurrentSession();
            GuiGridView gridView = (GuiGridView)SapSession.ActiveWindow.FindById(GridID, "GuiGridView");
            gridView.PressToolbarButton(ButtonName);
            return true;
        }

        public bool ButtonPressByID(string strControlID)
        {
            GuiSession SapSession = getCurrentSession();
            GuiButton button = (GuiButton)SapSession.ActiveWindow.FindById(strControlID, "GuiTextField");
            button.SetFocus();
            button.Press();
            return true;
        }

        public bool ButtonPressByID_Using_MainWindowName(string MainWindowName, string strControlID)
        {
            GuiSession SapSession = getCurrentSession(MainWindowName);
            //SapSession.SuppressBackendPopups = true;
            GuiButton button = (GuiButton)SapSession.ActiveWindow.FindById(strControlID, "GuiTextField");
            button.SetFocus();
            button.Press();
            //SapSession.SuppressBackendPopups = false;
            return true;
        }

        public void ButtonPress_UsingUIAutomation(string MainWindowName, string DialogBoxName, string ButtonName)
        {
            AutomationElement desktop = AutomationElement.RootElement;
            AutomationElement MainWindow = desktop.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, MainWindowName));
            AutomationElement BtnlogOn;

            if (DialogBoxName != "")
            {
                AutomationElement childWindow = MainWindow.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, DialogBoxName));
                BtnlogOn = childWindow.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, ButtonName));
            }
            else
            {
                BtnlogOn = MainWindow.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, ButtonName));
            }

            InvokePattern click = (InvokePattern)BtnlogOn.GetCurrentPattern(InvokePattern.Pattern);
            click.Invoke();
            //Marshal.FinalReleaseComObject(click);
            //Marshal.FinalReleaseComObject(BtnlogOn);
            //Marshal.FinalReleaseComObject(MainWindow);
            //Marshal.FinalReleaseComObject(childWindow);            
            //Marshal.FinalReleaseComObject(desktop);
            click = null;
            BtnlogOn = null;
            MainWindow = null;
            //childWindow = null;
            desktop = null;
            GC.Collect();
        }

        public String CheckAAError_IfError_Close()
        {
            //This function will check is there any Error from "Automation anywhere"
            //If any error then it get the error msg and close the error
            //if no error it will return "Not found"
            AutomationElement desktop = AutomationElement.RootElement;
            AutomationElement MainWindow = desktop.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, "Automation Anywhere Enterprise Client"));

            //Check is exist or not
            if (MainWindow == null)
            {
                desktop = null;
                MainWindow = null;
                GC.Collect();
                return "Not found";
            }

            //Get the Error msg
            AutomationElement txtErrorMsg = MainWindow.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.AutomationIdProperty, "txtMessage"));
            TextPattern txtPattern = (TextPattern)txtErrorMsg.GetCurrentPattern(TextPattern.Pattern);
            String strErrorMsg = txtPattern.DocumentRange.GetText(-1);
            AutomationElement BtnlogOn = MainWindow.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, "OK"));
            InvokePattern click = (InvokePattern)BtnlogOn.GetCurrentPattern(InvokePattern.Pattern);
            click.Invoke();

            click = null;
            BtnlogOn = null;
            MainWindow = null;
            desktop = null;
            GC.Collect();

            return strErrorMsg;
        }

        public string GetLabelText(string strControlID)
        {
            GuiSession SapSession = getCurrentSession();
            GuiLabel label = (GuiLabel)SapSession.ActiveWindow.FindById(strControlID, "GuiLabelField");
            label.SetFocus();

            return label.Text;
        }

        public string GetLabelText(string MainWindowName, string DialogBoxName1, string DialogBoxName2, string strControlID)
        {
            AutomationElement desktop = AutomationElement.RootElement;
            AutomationElement MainWindow = desktop.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, MainWindowName));

            if (DialogBoxName1 != "")
            {
                AutomationElement DialogBox1 = MainWindow.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, DialogBoxName1));
                if (DialogBoxName1 != "")
                {
                    AutomationElement DialogBox2 = DialogBox1.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, DialogBoxName2));
                    DialogBox2.SetFocus();
                }
                else
                {
                    DialogBox1.SetFocus();
                }
            }
            else
            {
                MainWindow.SetFocus();
            }

            GuiSession SapSession = getCurrentSession(MainWindowName);
            SapSession.SaveAsUnicode = true;
            var chld = SapSession.Children;
            for (int i = 0; i < chld.Count; i++)
            {
                Debug.Print((((GuiMainWindow)chld.Item(i)).Text));
            }

            GuiLabel label = (GuiLabel)SapSession.ActiveWindow.FindById(strControlID, "GuiLabelField");
            label.SetFocus();
            string strLabelText = label.Text;

            MainWindow = null;
            desktop = null;
            GC.Collect();

            return strLabelText;
        }

        public string GetLabelText_1(string MainWindowName, string DialogBoxName1, string DialogBoxName2, string strControlID)
        {
            AutomationElement desktop = AutomationElement.RootElement;
            AutomationElement MainWindow = desktop.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, MainWindowName));

            if (DialogBoxName1 != "")
            {
                AutomationElement DialogBox1 = MainWindow.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, DialogBoxName1));
                if (DialogBoxName1 != "")
                {
                    AutomationElement DialogBox2 = DialogBox1.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, DialogBoxName2));
                    DialogBox2.SetFocus();
                }
                else
                {
                    DialogBox1.SetFocus();
                }
            }
            else
            {
                MainWindow.SetFocus();
            }

            GuiSession SapSession = getCurrentSession(MainWindowName);
            SapSession.SaveAsUnicode = true;
            var chld = SapSession.Children;
            //for (int i = 0; i < chld.Count; i++)
            //{
            //    Debug.Print((((GuiMainWindow)chld.Item(i)).Text));
            //}

            GuiLabel label = (GuiLabel)SapSession.ActiveWindow.FindById(strControlID, "GuiLabelField");
            label.SetFocus();
            string strLabelText = label.Text;

            MainWindow = null;
            desktop = null;
            GC.Collect();

            return strLabelText;
        }

        public void ButtonPress_UsingUIAutomation_MultipleWindows(string MainWindowName, string DialogBoxName1, string DialogBoxName2, string ButtonName)
        {
            AutomationElement desktop = AutomationElement.RootElement;
            AutomationElement MainWindow = desktop.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, MainWindowName));
            AutomationElement DialogBox1 = MainWindow.FindFirst(TreeScope.Children, new AndCondition(new PropertyCondition(AutomationElement.NameProperty, DialogBoxName1), new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window)));
            AutomationElement DialogBox2 = DialogBox1.FindFirst(TreeScope.Children, new AndCondition(new PropertyCondition(AutomationElement.NameProperty, DialogBoxName2), new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window)));
            AutomationElement BtnlogOn = DialogBox2.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, ButtonName));
            InvokePattern click = (InvokePattern)BtnlogOn.GetCurrentPattern(InvokePattern.Pattern);
            click.Invoke();
            //Marshal.FinalReleaseComObject(click);
            //Marshal.FinalReleaseComObject(BtnlogOn);
            //Marshal.FinalReleaseComObject(MainWindow);
            //Marshal.FinalReleaseComObject(childWindow);            
            //Marshal.FinalReleaseComObject(desktop);
            click = null;
            BtnlogOn = null;
            MainWindow = null;
            DialogBox1 = null;
            DialogBox2 = null;
            desktop = null;
            GC.Collect();
        }

        public Boolean IsWindow_Exists_UsingUIAutomation_ThreeWindows(string MainWindowName, string DialogBoxName1, string DialogBoxName2)
        {
            Boolean blnReturnvalue = false;
            AutomationElement desktop = AutomationElement.RootElement;
            AutomationElement MainWindow = desktop.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, MainWindowName));
            AutomationElement DialogBox1 = MainWindow.FindFirst(TreeScope.Children, new AndCondition(new PropertyCondition(AutomationElement.NameProperty, DialogBoxName1), new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window)));
            AutomationElement DialogBox2 = null;
            try
            {
                DialogBox2 = DialogBox1.FindFirst(TreeScope.Children, new AndCondition(new PropertyCondition(AutomationElement.NameProperty, DialogBoxName2), new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window)));
            }
            catch { }

            if (DialogBox2 != null)
            {
                blnReturnvalue = true;
            }

            MainWindow = null;
            DialogBox1 = null;
            DialogBox2 = null;
            desktop = null;
            GC.Collect();

            return blnReturnvalue;
        }

        public Boolean IsWindow_Exists_UsingUIAutomation_TwoWindows(string MainWindowName, string DialogBoxName1)
        {
            Boolean blnReturnvalue = false;
            AutomationElement desktop = AutomationElement.RootElement;
            AutomationElement MainWindow = desktop.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, MainWindowName));
            AutomationElement DialogBox1 = null;


            try
            {
                DialogBox1 = DialogBox1.FindFirst(TreeScope.Children, new AndCondition(new PropertyCondition(AutomationElement.NameProperty, DialogBoxName1), new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window)));
            }
            catch { }

            if (DialogBox1 != null)
            {
                blnReturnvalue = true;
            }

            MainWindow = null;
            DialogBox1 = null;
            desktop = null;
            GC.Collect();

            return blnReturnvalue;
        }

        public string StatusText()
        {
            GuiSession SapSession = getCurrentSession();
            GuiStatusbar statusBar = (GuiStatusbar)SapSession.ActiveWindow.FindById("sbar");

            ////Adding log
            //System.IO.StreamWriter m_fswLogFile;
            //string strFilePath = @"E:\Excel Data\statustextlog.txt";
            //DateTime m_dtLastTimeLog = DateTime.MinValue;

            //m_fswLogFile = new System.IO.StreamWriter(strFilePath, true);
            //m_fswLogFile.AutoFlush = true;
            //m_fswLogFile.WriteLine("Satusbar text captured:" + statusBar.Text);

            //m_fswLogFile.Close();
            //m_fswLogFile = null;

            return statusBar.Text;
        }

        public Tuple<String,String> StatusTextAndMessageType()
        {
            GuiSession SapSession = getCurrentSession();
            GuiStatusbar statusBar = (GuiStatusbar)SapSession.ActiveWindow.FindById("sbar");

            ////Adding log
            //System.IO.StreamWriter m_fswLogFile;
            //string strFilePath = @"E:\Excel Data\statustextlog.txt";
            //DateTime m_dtLastTimeLog = DateTime.MinValue;

            //m_fswLogFile = new System.IO.StreamWriter(strFilePath, true);
            //m_fswLogFile.AutoFlush = true;
            //m_fswLogFile.WriteLine("Satusbar text captured: Type:" + statusBar.MessageType + " Status Text:" + statusBar.Text);

            //m_fswLogFile.Close();
            //m_fswLogFile = null;

            return new Tuple<string, string>(statusBar.MessageType, statusBar.Text);
        }

        public void LogOnButtonClick()
        {
            AutomationElement desktop = AutomationElement.RootElement;
            AutomationElement sapui = desktop.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, "SAP Logon 730"));
            AutomationElement BtnlogOn = sapui.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.AutomationIdProperty, "1068"));
            InvokePattern click = (InvokePattern)BtnlogOn.GetCurrentPattern(InvokePattern.Pattern);
            click.Invoke();
            click = null;
            BtnlogOn = null;
            sapui = null;
            desktop = null;
            GC.Collect();
        }

        public void SaveAsExcelWindow(string SAPWindowName, string strExcelFullPath)
        {
            //Get the save as/另存为 window
            AutomationElement desktop = AutomationElement.RootElement;
            AutomationElement sapwindow = desktop.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, SAPWindowName));
            AutomationElement saveas = sapwindow.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, "另存为"));

            //Input the excel full path
            AutomationElement filename = saveas.FindFirst(TreeScope.Descendants, new AndCondition(new PropertyCondition(AutomationElement.AutomationIdProperty, "1148"), new PropertyCondition(AutomationElement.ClassNameProperty, "Edit")));
            ValuePattern filenamepattern = (ValuePattern)filename.GetCurrentPattern(ValuePattern.Pattern);
            filenamepattern.SetValue(strExcelFullPath);
            System.Threading.Thread.Sleep(200);
            //Click save button
            AutomationElement btnSave = saveas.FindFirst(TreeScope.Children, new AndCondition(new PropertyCondition(AutomationElement.AutomationIdProperty, "1"), new PropertyCondition(AutomationElement.ClassNameProperty, "Button")));
            InvokePattern click = (InvokePattern)btnSave.GetCurrentPattern(InvokePattern.Pattern);
            click.Invoke();
            System.Threading.Thread.Sleep(500);

            //Check is there any confirm save as window
            click = null;
            btnSave = null;
            filename = null;
            saveas = null;
            sapwindow = null;
            desktop = null;
            GC.Collect();
        }

        private bool TextInput_NameEx(string strControlID, string strValue)
        {
            GuiSession SapSession = getCurrentSession();
            GuiTextField txtControl = (GuiTextField)SapSession.ActiveWindow.FindByNameEx(strControlID, 1);
            txtControl.SetFocus();
            txtControl.Text = strValue;
            return true;
        }

        //private bool SelectDropDownItem(this AutomationElement comboBoxElement, string item) {
        //    bool itemFound = false;
        //    AutomationElement elementList;
        //    CacheRequest cacheRequest = new CacheRequest();
        //    cacheRequest.Add(AutomationElement.NameProperty);
        //    cacheRequest.TreeScope = TreeScope.Element | TreeScope.Children;

        //    using (cacheRequest.Activate()) {
        //        // Load the list element and cache the specified properties for its descendants.
        //        Condition cond = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.List);
        //        elementList = comboBoxElement.FindFirst(TreeScope.Children, cond);
        //    }

        //    //Loop thru and find the actual ListItem
        //    foreach (AutomationElement child in elementList.CachedChildren)
        //        if (child.Cached.Name == item) {
        //            SelectionItemPattern select = (SelectionItemPattern)child.GetCurrentPattern(SelectionItemPattern.Pattern);                    
        //            select.Select();
        //            itemFound = true;
        //            break;
        //        }
        //    return itemFound;
        //}

        public void ComboSelect_UsingUIAutomation(string MainWindowName, string ComboBox_AutomationID, string SelectItemText)
        {
            AutomationElement desktop = AutomationElement.RootElement;
            AutomationElement MainWindow = desktop.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, MainWindowName));
            AutomationElement cboBox, cboItem;

            cboBox = MainWindow.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.AutomationIdProperty, ComboBox_AutomationID));
            //Click the combo box to expand the items
            ExpandCollapsePattern expandPattern = (ExpandCollapsePattern)cboBox.GetCurrentPattern(ExpandCollapsePattern.Pattern);
            expandPattern.Expand();

            //Search the item then click the item
            cboItem = cboBox.FindFirst(TreeScope.Subtree, new PropertyCondition(AutomationElement.NameProperty, SelectItemText));
            SelectionItemPattern itemSelect = (SelectionItemPattern)cboItem.GetCurrentPattern(SelectionItemPattern.Pattern);
            try
            {
                itemSelect.Select();
            }
            catch (Exception)
            {
                //Ignore this error, bcz some window will wait until the some action has been carried,
            }

            //InvokePattern click = (InvokePattern)cboItem.GetCurrentPattern(InvokePattern.Pattern);
            //click.Invoke();  
            //click = null;

            //Release the memory
            MainWindow = null;
            //childWindow = null;
            desktop = null;
            GC.Collect();
        }

        public void ComboBox_SelectItem_Using_Key(string ControlID, string ItemKey)
        {
            GuiSession SapSession = getCurrentSession();
            GuiComboBox gridView = (GuiComboBox)SapSession.ActiveWindow.FindById(ControlID, "GuiComboBox");
            gridView.Key = ItemKey;
        }

        public void ComboBox_SelectItem_Using_ItemTextcontains(string ControlID, string ItemTextContains)
        {
            GuiSession SapSession = getCurrentSession();
            GuiComboBox gridView = (GuiComboBox)SapSession.ActiveWindow.FindById(ControlID, "GuiComboBox");

            //GuiComponentCollection output = gridView.AccLabelCollection;

            //Console.WriteLine("AccText Collection");
            //foreach (GuiLabel o in output) {
            //    Console.WriteLine(o.Text);
            //}
            //var value = gridView.Value;
            //Console.WriteLine("Value:" + value);
            //var value1 = gridView.Text;
            //Console.WriteLine("Text:" + value1);
            //var acctext = gridView.Text;
            //Console.WriteLine("AccText:" + acctext);

            GuiCollection entries = gridView.Entries;
            String key = "";
            foreach (GuiComboBoxEntry o in entries)
            {
                if (o.Value.Contains(ItemTextContains))
                {
                    key = o.Key;
                    break;
                }
            }
            if (key == "") throw new Exception("The item text:" + ItemTextContains + " is not contains in any of combobox itemlist");
            gridView.Key = key;
        }

    }
}
