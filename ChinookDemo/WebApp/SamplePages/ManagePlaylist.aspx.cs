using System;
using System.Web.UI.WebControls;

#region Additonal Namespaces
//using WebApp.Security;
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

                //code to go here

        }

        protected void GenreFetch_Click(object sender, EventArgs e)
        {

                //code to go here

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
            //code to go here
 
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
            //code to go here
            
        }

    }
}