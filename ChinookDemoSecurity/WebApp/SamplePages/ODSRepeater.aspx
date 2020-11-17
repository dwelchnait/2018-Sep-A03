<%@ Page Title="ODS Repeater" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ODSRepeater.aspx.cs" Inherits="WebApp.SamplePages.ODSRepeater" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Repeater using ODS with nested query</h1>
    <div class="row">
        <div class="col-12">
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
        </div>
    </div>
    <%-- set up the parameter search input area --%>
    <div class="row">
        <div class="offset-1">
            <asp:Label ID="Label1" runat="server" 
                Text="Enter the lowest playlist track size desired:"></asp:Label>&nbsp;&nbsp;
            <asp:TextBox ID="PlayListSizeArg" runat="server"
                 TextMode="Number" step="1" min="0"
                 placeholder="0" ToolTip="Enter the desired playlist minimum size"></asp:TextBox>&nbsp;&nbsp;
            <asp:Button ID="Fetch" runat="server" Text="Fetch"
                CssClass="btn btn-primary" OnClick="Fetch_Click"/>
        </div>
    </div>
    <div class="row">
        <div class="offset-3">
            <%-- Repeater 
                
                ItemType will allow ou to select the definition of the
                   object that the data is using (classname)
                
                If you use ItemType, you can use Item. in referencing
                    your properties as you develop your control
                
                --%>
            <asp:Repeater ID="ClientPlayList" runat="server"
                DataSourceID="ClientPlayListODS"
                 ItemType="ChinookSystem.ViewModels.PlayListItem">
                <HeaderTemplate>
                    <h3>Client Playlist</h3>
                </HeaderTemplate>
                <ItemTemplate>
                    <%-- flat information on ODS --%>
                    <h4>PlayList Name: <%# Item.Name %> (<%# Item.TrackCount %>)</h4>
                    <br/>
                    <h5> Owner: <%# Item.UserName %></h5>

                    <%-- the internal list on each record in the ODS --%>
                    <%-- gridview 
                    <asp:GridView ID="PlayListSongs" runat="server"
                         DataSource="<%# Item.Songs %>" CssClass="table">
                    </asp:GridView>--%>
                    <%-- ListView 
                    <asp:ListView ID="PlayListSongs" runat="server"
                         DataSource="<%# Item.Songs %>"
                         ItemType="ChinookSystem.ViewModels.PlayListSong">
                        <ItemTemplate>
                            <span style="background-color:silver">
                                <%# Item.Song %> &nbsp;&nbsp; (<%# Item.GenreName %>)<br />
                            </span>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                             <span style="background-color:aquamarine">
                                <%# Item.Song %> &nbsp;&nbsp; (<%# Item.GenreName %>)<br />
                            </span>
                        </AlternatingItemTemplate>
                        <LayoutTemplate>
                            <span runat="server" id="itemPlaceholder"></span>
                        </LayoutTemplate>
                    </asp:ListView>--%>
                    <%-- Repeater 
                    <asp:Repeater ID="PlayListSongs" runat="server"
                         DataSource="<%# Item.Songs %>" 
                         ItemType="ChinookSystem.ViewModels.PlayListSong">
                        <ItemTemplate>
                             <span style="background-color:silver">
                                <%# Item.Song %> &nbsp;&nbsp; (<%# Item.GenreName %>)<br />
                            </span>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <span style="background-color:aquamarine">
                                <%# Item.Song %> &nbsp;&nbsp; (<%# Item.GenreName %>)<br />
                            </span>
                        </AlternatingItemTemplate>
                    </asp:Repeater>--%>
                    <%-- Table using Repeater --%>
                    <table>
                        <asp:Repeater ID="PlayListSongs" runat="server"
                         DataSource="<%# Item.Songs %>" 
                         ItemType="ChinookSystem.ViewModels.PlayListSong">
                        <ItemTemplate>
                            <tr>
                                <td style="background-color:silver">
                                <%# Item.Song %> 
                                </td>
                                <td style="background-color:silver">
                                    (<%# Item.GenreName %>)
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <td style="background-color:aquamarine">
                                    <%# Item.Song %> 
                                </td>
                                <td style="background-color:aquamarine">
                                    (<%# Item.GenreName %>)
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                    </table>
                </ItemTemplate>
                <SeparatorTemplate>
                    <hr style="height:5px;" />
                </SeparatorTemplate>
            </asp:Repeater>
        </div>
    </div>


    <%-- ODS controls --%>
    <asp:ObjectDataSource ID="ClientPlayListODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="PlayList_GetPlayListOfSize" 
         OnSelected="SelectCheckForException"
        TypeName="ChinookSystem.BLL.PlayListController">
        <SelectParameters>
            <asp:ControlParameter ControlID="PlayListSizeArg" 
                PropertyName="Text" DefaultValue="0" 
                Name="lowestplaylistsize" Type="Int32"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
