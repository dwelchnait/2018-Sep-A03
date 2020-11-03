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
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Track Movement",
                    "You must have a play list name.");
            }
            else
            {
                if (PlayList.Rows.Count == 0)
                {
                    MessageUserControl.ShowInfo("Track Movement",
                    "You must have a play list showing.");
                }
                else
                {
                    //was anything actually selected
                    CheckBox songSelected = null; //reference pointer to a control
                    int rowsSelected = 0; //count number of songs selected
                    int trackid = 0; //trackid of the song to move
                    int tracknumber = 0; //tracknumber of the song to move

                    //traverse the song list
                    //only 1 song may be selected for movement
                    for (int index = 0; index < PlayList.Rows.Count; index++)
                    {
                        //point to a checkbox on the gridview row
                        songSelected = PlayList.Rows[index].FindControl("Selected") as CheckBox;
                        //Selected???
                        if (songSelected.Checked)
                        {
                            rowsSelected++;
                            trackid = int.Parse((PlayList.Rows[index].FindControl("TrackId") as Label).Text);
                            tracknumber = int.Parse((PlayList.Rows[index].FindControl("TrackNumber") as Label).Text);
                        }
                    }
                    //did you select a row and only a single row
                    if (rowsSelected != 1)
                    {
                        MessageUserControl.ShowInfo("Track Movement",
                            "You must select a single song to move.");
                    }
                    else
                    {
                        //is this the bottom row??
                        if (tracknumber == PlayList.Rows.Count)
                        {
                            MessageUserControl.ShowInfo("Track Movement",
                            "Song is at the bottom of the list already. No move is necessary.");
                        }
                        else
                        {
                            //move the track
                            MoveTrack(trackid, tracknumber, "down");
                        }
                    }
                }
            }

        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Track Movement",
                    "You must have a play list name.");
            }
            else
            {
                if (PlayList.Rows.Count == 0)
                {
                    MessageUserControl.ShowInfo("Track Movement",
                    "You must have a play list showing.");
                }
                else
                {
                    //was anything actually selected
                    CheckBox songSelected = null; //reference pointer to a control
                    int rowsSelected = 0; //count number of songs selected
                    int trackid = 0; //trackid of the song to move
                    int tracknumber = 0; //tracknumber of the song to move

                    //traverse the song list
                    //only 1 song may be selected for movement
                    for (int index=0; index < PlayList.Rows.Count; index++)
                    {
                        //point to a checkbox on the gridview row
                        songSelected = PlayList.Rows[index].FindControl("Selected") as CheckBox;
                        //Selected???
                        if (songSelected.Checked)
                        {
                            rowsSelected++;
                            trackid = int.Parse((PlayList.Rows[index].FindControl("TrackId") as Label).Text);
                            tracknumber = int.Parse((PlayList.Rows[index].FindControl("TrackNumber") as Label).Text);
                        }
                    }
                    //did you select a row and only a single row
                    if (rowsSelected != 1)
                    {
                        MessageUserControl.ShowInfo("Track Movement",
                            "You must select a single song to move.");
                    }
                    else
                    {
                        //is this the top row??
                        if (tracknumber == 1)
                        {
                            MessageUserControl.ShowInfo("Track Movement",
                            "Song is at the top of the list already. No move is necessary.");
                        }
                        else
                        {
                            //move the track
                            MoveTrack(trackid, tracknumber, "up");
                        }
                    }
                }
            }
        }

        protected void MoveTrack(int trackid, int tracknumber, string direction)
        {
            string username = "HansenB";
            MessageUserControl.TryRun(() =>
            {
                PlaylistTracksController sysmgr = new PlaylistTracksController();
                sysmgr.MoveTrack(username, PlaylistName.Text, trackid, tracknumber, direction);
                List<UserPlaylistTrack> info = sysmgr.List_TracksForPlaylist(PlaylistName.Text, username);
                PlayList.DataSource = info;
                PlayList.DataBind();
            },"Move Track", "Track has been move");
 
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