using System.Activities.Presentation.Metadata;
using System.ComponentModel;
using System.ComponentModel.Design;
using VJ.SAPGUIScript.Activities.Design.Designers;
using VJ.SAPGUIScript.Activities.Design.Properties;

namespace VJ.SAPGUIScript.Activities.Design
{
    public class DesignerMetadata : IRegisterMetadata
    {
        public void Register()
        {
            var builder = new AttributeTableBuilder();
            builder.ValidateTable();

            var categoryAttribute = new CategoryAttribute($"{Resources.Category}");

            builder.AddCustomAttributes(typeof(Login), categoryAttribute);
            builder.AddCustomAttributes(typeof(Login), new DesignerAttribute(typeof(LoginDesigner)));
            builder.AddCustomAttributes(typeof(Login), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(TCode_Input), categoryAttribute);
            builder.AddCustomAttributes(typeof(TCode_Input), new DesignerAttribute(typeof(TCode_InputDesigner)));
            builder.AddCustomAttributes(typeof(TCode_Input), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(TextBox_Input), categoryAttribute);
            builder.AddCustomAttributes(typeof(TextBox_Input), new DesignerAttribute(typeof(TextBox_InputDesigner)));
            builder.AddCustomAttributes(typeof(TextBox_Input), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(RadioBox_Click), categoryAttribute);
            builder.AddCustomAttributes(typeof(RadioBox_Click), new DesignerAttribute(typeof(RadioBox_ClickDesigner)));
            builder.AddCustomAttributes(typeof(RadioBox_Click), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(Button_Click), categoryAttribute);
            builder.AddCustomAttributes(typeof(Button_Click), new DesignerAttribute(typeof(Button_ClickDesigner)));
            builder.AddCustomAttributes(typeof(Button_Click), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(CheckBox_ClickByID), categoryAttribute);
            builder.AddCustomAttributes(typeof(CheckBox_ClickByID), new DesignerAttribute(typeof(CheckBox_ClickByIDDesigner)));
            builder.AddCustomAttributes(typeof(CheckBox_ClickByID), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(Menu_SelectionByID), categoryAttribute);
            builder.AddCustomAttributes(typeof(Menu_SelectionByID), new DesignerAttribute(typeof(Menu_SelectionByIDDesigner)));
            builder.AddCustomAttributes(typeof(Menu_SelectionByID), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(TableRow_Select), categoryAttribute);
            builder.AddCustomAttributes(typeof(TableRow_Select), new DesignerAttribute(typeof(TableRow_SelectDesigner)));
            builder.AddCustomAttributes(typeof(TableRow_Select), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(TreeView_Select), categoryAttribute);
            builder.AddCustomAttributes(typeof(TreeView_Select), new DesignerAttribute(typeof(TreeView_SelectDesigner)));
            builder.AddCustomAttributes(typeof(TreeView_Select), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(ContextMenuSelectItem), categoryAttribute);
            builder.AddCustomAttributes(typeof(ContextMenuSelectItem), new DesignerAttribute(typeof(ContextMenuSelectItemDesigner)));
            builder.AddCustomAttributes(typeof(ContextMenuSelectItem), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(Grid_SelectRow_AndDoubleClick), categoryAttribute);
            builder.AddCustomAttributes(typeof(Grid_SelectRow_AndDoubleClick), new DesignerAttribute(typeof(Grid_SelectRow_AndDoubleClickDesigner)));
            builder.AddCustomAttributes(typeof(Grid_SelectRow_AndDoubleClick), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(Grid_SelectCell_AndDoubleClick), categoryAttribute);
            builder.AddCustomAttributes(typeof(Grid_SelectCell_AndDoubleClick), new DesignerAttribute(typeof(Grid_SelectCell_AndDoubleClickDesigner)));
            builder.AddCustomAttributes(typeof(Grid_SelectCell_AndDoubleClick), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(ComboBox_SelectItem_Using_Key), categoryAttribute);
            builder.AddCustomAttributes(typeof(ComboBox_SelectItem_Using_Key), new DesignerAttribute(typeof(ComboBox_SelectItem_Using_KeyDesigner)));
            builder.AddCustomAttributes(typeof(ComboBox_SelectItem_Using_Key), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(ComboBox_SelectItem_Using_ItemTextcontains), categoryAttribute);
            builder.AddCustomAttributes(typeof(ComboBox_SelectItem_Using_ItemTextcontains), new DesignerAttribute(typeof(ComboBox_SelectItem_Using_ItemTextcontainsDesigner)));
            builder.AddCustomAttributes(typeof(ComboBox_SelectItem_Using_ItemTextcontains), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(Get_TextBox_Value), categoryAttribute);
            builder.AddCustomAttributes(typeof(Get_TextBox_Value), new DesignerAttribute(typeof(Get_TextBox_ValueDesigner)));
            builder.AddCustomAttributes(typeof(Get_TextBox_Value), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(Grid_GetCellValue), categoryAttribute);
            builder.AddCustomAttributes(typeof(Grid_GetCellValue), new DesignerAttribute(typeof(Grid_GetCellValueDesigner)));
            builder.AddCustomAttributes(typeof(Grid_GetCellValue), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(Table_GetCellValue), categoryAttribute);
            builder.AddCustomAttributes(typeof(Table_GetCellValue), new DesignerAttribute(typeof(Table_GetCellValueDesigner)));
            builder.AddCustomAttributes(typeof(Table_GetCellValue), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(Get_TextEditBox_Value), categoryAttribute);
            builder.AddCustomAttributes(typeof(Get_TextEditBox_Value), new DesignerAttribute(typeof(Get_TextEditBox_ValueDesigner)));
            builder.AddCustomAttributes(typeof(Get_TextEditBox_Value), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(Get_StatusBar_Text), categoryAttribute);
            builder.AddCustomAttributes(typeof(Get_StatusBar_Text), new DesignerAttribute(typeof(Get_StatusBar_TextDesigner)));
            builder.AddCustomAttributes(typeof(Get_StatusBar_Text), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(TreeItem_DoubleClick), categoryAttribute);
            builder.AddCustomAttributes(typeof(TreeItem_DoubleClick), new DesignerAttribute(typeof(TreeItem_DoubleClickDesigner)));
            builder.AddCustomAttributes(typeof(TreeItem_DoubleClick), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(LoginSSO), categoryAttribute);
            builder.AddCustomAttributes(typeof(LoginSSO), new DesignerAttribute(typeof(LoginSSODesigner)));
            builder.AddCustomAttributes(typeof(LoginSSO), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(GetLabelText), categoryAttribute);
            builder.AddCustomAttributes(typeof(GetLabelText), new DesignerAttribute(typeof(GetLabelTextDesigner)));
            builder.AddCustomAttributes(typeof(GetLabelText), new HelpKeywordAttribute(""));


            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}
