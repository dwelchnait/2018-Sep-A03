using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additonal Namespaces
//using WebApp.Security;
using ChinookSystem.BLL;
using ChinookSystem.ViewModels;
#endregion

namespace WebApp.SamplePages
{
    public partial class ManagePlaylist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TracksSelectionList.DataSource = null;
        }

        #region  Error Handling
        protected void SelectCheckForException(object sender,
                                       ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }
        protected void InsertCheckForException(object sender,
                                              ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                MessageUserControl.ShowInfo("Success", "Album has been added.");
            }
            else
            {
                MessageUserControl.HandleDataBoundException(e);
            }
        }
        protected void UpdateCheckForException(object sender,
                                               ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                MessageUserControl.ShowInfo("Success", "Album has been updated.");
            }
            else
            {
                MessageUserControl.HandleDataBoundException(e);
            }
        }
        protected void DeleteCheckForException(object sender,
                                                ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                MessageUserControl.ShowInfo("Success", "Album has been removed.");
            }
            else
            {
                MessageUserControl.HandleDataBoundException(e);
            }
        }

        #endregion


        protected void ArtistFetch_Click(object sender, EventArgs e)
        {
            TracksBy.Text = "Artist";
            SearchArg.Text = ArtistName.Text;
            //validate that data exists if not put out a message
            if (string.IsNullOrEmpty(ArtistName.Text))
            {
                MessageUserControl.ShowInfo("Search entry error",
                    "Enter a artist name or partial artist name. The press your button.");
                SearchArg.Text = "xcfdrte";
            }

              
            //to force the listview to rebind (to execute again)
            //NOTE there is NO DataSource assignment as that is
            //     accomplished using the ODS and a DataSourceID parameter
            //     on the ListView control
            TracksSelectionList.DataBind();



        }

        protected void MediaTypeFetch_Click(object sender, EventArgs e)
        {

            TracksBy.Text = "MediaType";
            //the ddl does not have a prompt line, therefore
            //    to selection test is requried
            //remember: SelectedValue returns contents as a string
            SearchArg.Text = MediaTypeDDL.SelectedValue;
 
            //to force the listview to rebind (to execute again)
            //NOTE there is NO DataSource assignment as that is
            //     accomplished using the ODS and a DataSourceID parameter
            //     on the ListView control
            TracksSelectionList.DataBind();

        }

        protected void GenreFetch_Click(object sender, EventArgs e)
        {
            TracksBy.Text = "Genre";
          
            SearchArg.Text = GenreDDL.SelectedValue;

            TracksSelectionList.DataBind();
        }

        protected void AlbumFetch_Click(object sender, EventArgs e)
        {

            TracksBy.Text = "Album";
            SearchArg.Text = AlbumTitle.Text;
            //validate that data exists if not put out a message
            if (string.IsNullOrEmpty(AlbumTitle.Text))
            {
                MessageUserControl.ShowInfo("Search entry error",
                    "Enter a album title or partial album title. The press your button.");
                SearchArg.Text = "xcfdrte";
            }


            //to force the listview to rebind (to execute again)
            //NOTE there is NO DataSource assignment as that is
            //     accomplished using the ODS and a DataSourceID parameter
            //     on the ListView control
            TracksSelectionList.DataBind();

        }

        protected void PlayListFetch_Click(object sender, EventArgs e)
        {
            //security is yet to be implemented
            //this page needs to know the username of the currently logged user
            //temporarily we will hard code the username
            string username = "HansenB";

            //validate that a string exists in the playlist name
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Missing data",
                    "Enter the playlist name.");
            }
            else
            {
                //how do we do error handling using MessageUsercontrol if the
                //    code executing is NOT part of ODS
                //you could use Try/Catch (but we won't)
                //we wish to use MessageUserControl
                //if you examine the source code for MessageUserControl, you will
                //   find embedded within the code the Try/Catch
                //The syntax:
                //   MessageUserControl.TryRun( () => {your code block});
                //   MessageUserControl.TryRun( () => {your code block},"Success Title","Success message");

                MessageUserControl.TryRun(() =>
                {
                    //standard lookup coding block

                    //connect to the controller
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    //issue the controller call
                    List<UserPlaylistTrack> info = sysmgr.List_TracksForPlaylist(PlaylistName.Text, username);
                    //assign the results to the control
                    PlayList.DataSource = info;
                    //bind results to control
                    PlayList.DataBind();
                },"PlayList","View the current songs on the playlist");
                
            }
 
        }

        protected void MoveDown_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void MoveTrack(int trackid, int tracknumber, string direction)
        {
            //call BLL to move track
 
        }


        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void TracksSelectionList_ItemCommand(object sender, 
            ListViewCommandEventArgs e)
        {
            string username = "HansenB";
            //validation of incoming data
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Missing Data", "Enter the playlist name");
            }
            else
            {
                //Reminder: MessageUserControl will do the error handling
                MessageUserControl.TryRun(() =>
                {
                    //coding block for your logic to be run under the error handling
                    //  control of MessageUserControl
                    //a standard add to the database
                    //connect to controller
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    //issue the call to the controller method
                    sysmgr.Add_TrackToPLaylist(PlaylistName.Text, username,
                        int.Parse(e.CommandArgument.ToString()));
                    //refresh the playlist
                    List<UserPlaylistTrack> info = sysmgr.List_TracksForPlaylist(PlaylistName.Text, username);
                    PlayList.DataSource = info;
                    PlayList.DataBind();
                }, "Add track to Playlist", "Track has been added to the playlist");
            }
            
        }

    }
}