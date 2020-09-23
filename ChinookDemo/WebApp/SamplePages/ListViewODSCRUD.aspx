<%@ Page Title="Listview ODS CRUD" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListViewODSCRUD.aspx.cs" Inherits="WebApp.SamplePages.ListViewODSCRUD" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" 
    TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Single control ODS CRUD : ListView</h1>
    <blockquote>
        this sample will use the asp:ListView control<br/>
        this sample will use ObjectDataSource for the control<br />
        this sample will use minimal code behind<br />
        this sample will demonstration the user of the course supplied error handling control<br />
        this sample will demonstration validation on a ListView CRUD
    </blockquote>
    <div class="row">
        <%-- using the MessageUserControl to handle errors on a web page --%>
        <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
        <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" 
             HeaderText="Following are concerns with your data:"
             ValidationGroup="igroup"/>
        <asp:ValidationSummary ID="ValidationSummaryEdit" runat="server" 
            HeaderText="Following are concerns with your data:"
             ValidationGroup="egroup"/>
    </div>
    <div class="row">
       <%-- <div class="offset-1">--%>
            <%-- reminder to use the attribute DataKeyNames to get the Delete to work. --%>
            <asp:ListView ID="AlbumList" runat="server" 
                DataSourceID="AlbumListODS" 
                InsertItemPosition="FirstItem"
                 DataKeyNames="AlbumId">

                <AlternatingItemTemplate>
                    <tr style="background-color: #FFFFFF; color: #284775;">
                        <td>
                            <asp:Button runat="server" CommandName="Delete" Text="Delete" 
                                ID="DeleteButton" OnClientClick="return confirm('Are you sure you wish to remove.')" />
                            <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                        </td>
                        <td>
                            <asp:Label Text='<%# Eval("AlbumId") %>' runat="server" 
                                ID="AlbumIdLabel" Width="30px" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Title") %>' runat="server" 
                                ID="TitleLabel" /></td>
                        <td>
                            <asp:DropDownList ID="ArtistList" runat="server" 
                                DataSourceID="ArtistListODS" 
                                DataTextField="DisplayText" 
                                DataValueField="ValueId"
                                 selectedValue='<%# Eval("ArtistId") %>'
                                 Enabled="false" Width="300px">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label Text='<%# Eval("ReleaseYear") %>' runat="server" 
                                ID="ReleaseYearLabel" Width="70px"/></td>
                        <td>
                            <asp:Label Text='<%# Eval("ReleaseLabel") %>' runat="server" 
                                ID="ReleaseLabelLabel" /></td>
                    </tr>
                </AlternatingItemTemplate>
                <EditItemTemplate>
                    <asp:RequiredFieldValidator ID="RequiredTitleTextBoxE" runat="server"
                         ErrorMessage="Title is required" Display="None"
                         ControlToValidate="TitleTextBoxE" ValidationGroup="egroup">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegExTitleTextBoxE" runat="server" 
                        ErrorMessage="Title is limited to 160 characters" Display="None"
                         ControlToValidate="TitleTextBoxE" ValidationGroup="egroup"
                         ValidationExpression="^.{1,160}$">
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredReleaseYearTextBoxE" runat="server"
                         ErrorMessage="Year is required" Display="None"
                         ControlToValidate="ReleaseYearTextBoxE" ValidationGroup="egroup">
                    </asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeReleaseYearTextBoxE" runat="server" 
                        ErrorMessage="Release Year must be between 1950 and this year" Display="None"
                         MinimumValue="1950" MaximumValue="<%# DateTime.Today.Year %>"
                         ControlToValidate="ReleaseYearTextBoxE" ValidationGroup="egroup">
                    </asp:RangeValidator>
                    <asp:RegularExpressionValidator ID="RegExReleaseLabelTextBoxE" runat="server" 
                        ErrorMessage="Label is limited to 50 characters" Display="None"
                         ControlToValidate="ReleaseLabelTextBoxE" ValidationGroup="egroup"
                         ValidationExpression="^.{1,50}$">
                    </asp:RegularExpressionValidator>
                    <tr style="background-color: #999999;">
                        <td>
                            <asp:Button runat="server" CommandName="Update" Text="Update" 
                                ID="UpdateButton" ValidationGroup="egroup" />
                            <asp:Button runat="server" CommandName="Cancel" Text="Cancel" ID="CancelButton" />
                        </td>
                        <td>
                            <asp:TextBox Text='<%# Bind("AlbumId") %>' runat="server" 
                                ID="AlbumIdTextBox" Width="30px" Enabled="false"/></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("Title") %>' runat="server" 
                                ID="TitleTextBoxE" /></td>
                        <td>
                            <asp:DropDownList ID="ArtistList" runat="server" 
                                DataSourceID="ArtistListODS" 
                                DataTextField="DisplayText" 
                                DataValueField="ValueId"
                                 selectedValue='<%# Bind("ArtistId") %>'
                                 Width="300px">
                            </asp:DropDownList></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("ReleaseYear") %>' runat="server" 
                                ID="ReleaseYearTextBoxE" Width="70px"/></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("ReleaseLabel") %>' runat="server" 
                                ID="ReleaseLabelTextBoxE" /></td>
                    </tr>
                </EditItemTemplate>
                <EmptyDataTemplate>
                    <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                        <tr>
                            <td>No data was returned.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <InsertItemTemplate>
                     <asp:RequiredFieldValidator ID="RequiredTitleTextBoxI" runat="server"
                         ErrorMessage="Title is required" Display="None"
                         ControlToValidate="TitleTextBoxI" ValidationGroup="igroup">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegExTitleTextBoxI" runat="server" 
                        ErrorMessage="Title is limited to 160 characters" Display="None"
                         ControlToValidate="TitleTextBoxI" ValidationGroup="igroup"
                         ValidationExpression="^.{1,160}$">
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredReleaseYearTextBoxI" runat="server"
                         ErrorMessage="Year is required" Display="None"
                         ControlToValidate="ReleaseYearTextBoxI" ValidationGroup="igroup">
                    </asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeReleaseYearTextBoxI" runat="server" 
                        ErrorMessage="Release Year must be between 1950 and this year" Display="None"
                         MinimumValue="1950" MaximumValue="<%# DateTime.Today.Year %>"
                         ControlToValidate="ReleaseYearTextBoxI" ValidationGroup="igroup">
                    </asp:RangeValidator>
                    <asp:RegularExpressionValidator ID="RegExReleaseLabelTextBoxI" runat="server" 
                        ErrorMessage="Label is limited to 50 characters" Display="None"
                         ControlToValidate="ReleaseLabelTextBoxI" ValidationGroup="igroup"
                         ValidationExpression="^.{1,50}$">
                    </asp:RegularExpressionValidator>
                    <tr style="">
                        <td>
                            <asp:Button runat="server" CommandName="Insert" Text="Insert" 
                                ID="InsertButton"  ValidationGroup="igroup"/>
                            <asp:Button runat="server" CommandName="Cancel" Text="Clear" ID="CancelButton" />
                        </td>
                        <td>
                            <asp:TextBox Text='<%# Bind("AlbumId") %>' runat="server" 
                                ID="AlbumIdTextBox" Width="30px" Enabled="false"/></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("Title") %>' runat="server" ID="TitleTextBoxI" /></td>
                        <td>
                            <asp:DropDownList ID="ArtistList" runat="server" 
                                DataSourceID="ArtistListODS" 
                                DataTextField="DisplayText" 
                                DataValueField="ValueId"
                                 selectedValue='<%# Bind("ArtistId") %>'
                                 Width="300px">
                            </asp:DropDownList></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("ReleaseYear") %>' runat="server" 
                                ID="ReleaseYearTextBoxI" Width="70px"/></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("ReleaseLabel") %>' runat="server" 
                                ID="ReleaseLabelTextBoxI" /></td>
                    </tr>
                </InsertItemTemplate>
                <ItemTemplate>
                    <tr style="background-color: #E0FFFF; color: #333333;">
                        <td>
                            <asp:Button runat="server" CommandName="Delete" Text="Delete" 
                                ID="DeleteButton" OnClientClick="return confirm('Are you sure you wish to remove.')"/>
                            <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                        </td>
                        <td>
                            <asp:Label Text='<%# Eval("AlbumId") %>' runat="server" 
                                ID="AlbumIdLabel" Width="30px"/></td>
                        <td>
                            <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" /></td>
                        <td>
                            <asp:DropDownList ID="ArtistList" runat="server" 
                                DataSourceID="ArtistListODS" 
                                DataTextField="DisplayText" 
                                DataValueField="ValueId"
                                 selectedValue='<%# Eval("ArtistId") %>'
                                 Enabled="false" Width="300px">
                            </asp:DropDownList></td>
                        <td>
                            <asp:Label Text='<%# Eval("ReleaseYear") %>' runat="server" 
                                ID="ReleaseYearLabel" Width="70px"/></td>
                        <td>
                            <asp:Label Text='<%# Eval("ReleaseLabel") %>' runat="server" ID="ReleaseLabelLabel" /></td>
                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table runat="server">
                        <tr runat="server">
                            <td runat="server">
                                <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                                    <tr runat="server" style="background-color: #E0FFFF; color: #333333;">
                                        <th runat="server"></th>
                                        <th runat="server">Id</th>
                                        <th runat="server">Title</th>
                                        <th runat="server">Artist</th>
                                        <th runat="server">Year</th>
                                        <th runat="server">Label</th>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder"></tr>
                                </table>
                            </td>
                        </tr>
                        <tr runat="server">
                            <td runat="server" style="text-align: center; background-color: #C0C0C0; 
                                        font-family: Verdana, Arial, Helvetica, sans-serif; color: #FFFFFF">
                                <asp:DataPager runat="server" ID="DataPager1">
                                    <Fields>
                                        <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" 
                                            ShowNextPageButton="False" ShowPreviousPageButton="False"
                                             ButtonCssClass="datapagerStyle"></asp:NextPreviousPagerField>
                                        <asp:NumericPagerField></asp:NumericPagerField>
                                        <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" 
                                            ShowNextPageButton="False" ShowPreviousPageButton="False"
                                             ButtonCssClass="datapagerStyle"></asp:NextPreviousPagerField>
                                    </Fields>
                                </asp:DataPager>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
                <SelectedItemTemplate>
                    <tr style="background-color: #E2DED6; font-weight: bold; color: #333333;">
                        <td>
                            <asp:Button runat="server" CommandName="Delete" Text="Delete" 
                                ID="DeleteButton" OnClientClick="return confirm('Are you sure you wish to remove.')"/>
                            <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                        </td>
                        <td>
                            <asp:Label Text='<%# Eval("AlbumId") %>' runat="server" 
                                ID="AlbumIdLabel" Width="30px"/></td>
                        <td>
                            <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" /></td>
                        <td>
                            <asp:DropDownList ID="ArtistList" runat="server" 
                                DataSourceID="ArtistListODS" 
                                DataTextField="DisplayText" 
                                DataValueField="ValueId"
                                 selectedValue='<%# Eval("ArtistId") %>'
                                 Enabled="false" Width="300px">
                            </asp:DropDownList></td>
                        <td>
                            <asp:Label Text='<%# Eval("ReleaseYear") %>' runat="server" 
                                ID="ReleaseYearLabel" Width="70px"/></td>
                        <td>
                            <asp:Label Text='<%# Eval("ReleaseLabel") %>' runat="server" ID="ReleaseLabelLabel" /></td>
                    </tr>
                </SelectedItemTemplate>
            </asp:ListView>
       <%-- </div>--%>
    </div>
    <asp:ObjectDataSource ID="AlbumListODS" runat="server" 
        DataObjectTypeName="ChinookSystem.ViewModels.AlbumList" 
        DeleteMethod="Album_Delete" 
        InsertMethod="Album_Add"  
        SelectMethod="Album_List"
        UpdateMethod="Album_Update"
        OldValuesParameterFormatString="original_{0}" 
        TypeName="ChinookSystem.BLL.AlbumController"
         OnDeleted="DeleteCheckForException"
         OnInserted="InsertCheckForException"
         OnSelected="SelectCheckForException"
         OnUpdated="UpdateCheckForException">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ArtistListODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Artist_List" 
        TypeName="ChinookSystem.BLL.ArtistController"
         OnSelected="SelectCheckForException">
    </asp:ObjectDataSource>
</asp:Content>
