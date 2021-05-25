using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.db
{
    public class Message
    {
        public const String msgErrCommon = "Sorry!!! Some error occurred.";

        #region Event Message
        public const String msgSaveDelete = "Record deleted successfully.";
        public const String msgSaveDuplicate = "Record already exists.";
        public const String msgSaveEdit = "Record updated successfully.";
        public const String msgSaveErr = "Sorry! Data not saved, Data error.";
        public const String msgSaveErrDtls = "Sorry! Data not saved, Data error. Error : ";
        public const String msgSaveNew = "New record saved successfully.";
        public const String msgSaveStatus = "Record status updated successfully.";
        public const String msgErrDdlPop = " list population failed.";
        public const String msgErrDtlsPop = " But details population failed.";
        public const String msgErrEditPop = "Record detail population failed.";
        public const String msgErrFileSelect = "Please select file for upload.";
        public const String msgErrFileSave = " But file is not saved.";
        public const String msgErrListPop = " But list population failed due to ";
        public const String msgErrPriorDelete = " Exclude item(s) due to prior deletion.";
        public const String msgErrReqParam = "Required parameter error."; //Parameter name : 
        public const String msgErrSession = "Session Error.";
        public const String msgErrStatus = "Record status updation error.";
        public const String msgErrStock = " Exclude item(s) as unavailable in stock.";
        public const String msgErrInUse = " is already in use.";
        public const String msgPageInvalid = "Please check all required fields are filled up properly.";
        public const String msgPageRefresh = "Sorry! Data not saved, Due To Page Refresh";
        public const String msgNotFound = "No record found.";
        //public const String msgLotCuttingCompleted = "Lot cutting is completed. No further change is allowed.";
        public const String msgLotCompleted = "All planned work for this lot is over.";
        public const String msgValidation = "Record already exists.";
        public const String msgDelete = "Sorry! Data not Delete.";
        #endregion

        #region Page Processing Error Message
        public const String msgErrPage = "Sorry, an error occurred while processing your request.";
        public const String msgErr401 = "Invalid credentials.";
        public const String msgErr403 = "Forbidden, You do not have access.";
        public const String msgErr404 = "The requested URL was not found on this server.";
        public const String msgErrJS = "Please enable javascript in your browser.";
        #endregion

        #region Module Name
        public const String modName1 = "System User ";

        public const String modName21 = "Press Caption ";
        public const String modName22 = "Add Language ";
        public const String modName23 = "Add Hue ";
        public const String modName24 = "Category ";
        public const String modName25 = "Admin Panel ";
        public const String modName26 = "Ledger Group ";
        public const String modName27 = "Ledger SubGroup ";
        public const String modName28 = "Ledger ";
        public const String modName29 = "Press Position ";
        public const String modName31 = "Space ";
        public const String modName32 = "Space Type ";
        public const String modName33 = "Location ";

        public const String modName35 = "Edition ";
        public const String modName36 = "Staff ";

        public const String modName40 = "Channel ";
        public const String modName41 = "Release Order ";
        public const String modName42 = "Program ";
        public const String modName43 = "Zone ";
        public const String modName44 = "Type ";
        public const String modName45 = "Opening Raw Item ";

        public const String modName60 = "Lot Creation ";
        public const String modName61 = "Production-Consumption ";
        public const String modName62 = "Cutting Challan Issue ";
        public const String modName63 = "Cutting Challan Receipt ";
        public const String modName64 = "Job Work Challan Issue ";
        public const String modName65 = "Job Work Challan Receipt ";
        public const String modName67 = "Purchase Return ";
        public const String modName68 = "Opening Ledger ";
        public const String modName69 = "Sale Return ";
        #endregion
    }
}
