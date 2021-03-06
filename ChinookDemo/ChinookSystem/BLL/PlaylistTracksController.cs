﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.ViewModels;
using ChinookSystem.DAL;
using System.ComponentModel;
using ChinookSystem.Entities;
using DMIT2018Common.UserControls;
#endregion

namespace ChinookSystem.BLL
{
    public class PlaylistTracksController
    {
        public List<UserPlaylistTrack> List_TracksForPlaylist(
            string playlistname, string username)
        {
            using (var context = new ChinookSystemContext())
            {
                var results = from x in context.PlaylistTracks
                              where x.Playlist.Name.Equals(playlistname)
                                 && x.Playlist.UserName.Equals(username)
                              orderby x.TrackNumber
                              select new UserPlaylistTrack
                              {
                                  TrackID = x.TrackId,
                                  TrackNumber = x.TrackNumber,
                                  TrackName = x.Track.Name,
                                  Milliseconds = x.Track.Milliseconds,
                                  UnitPrice = x.Track.UnitPrice
                              };
                return results.ToList();
            }
        }//eom
        public void Add_TrackToPLaylist(string playlistname, string username, int trackid)
        {
            using (var context = new ChinookSystemContext())
            {
                //the code within this using wil be done as a Transaction which means
                //  there will be ONLY ONE .SaveChanges() within this code.
                //if the .SaveChanges is NOT executed successful, all work
                //   within this method will be rollback automatically!

                //trx
                //query: PlayList to see if list name exists
                //if not
                //  create an instance of Playlist
                //  load with data
                //  stage the instance for adding
                //  set the tracknumber to 1
                //if yes
                //  check to see if track already exists on playlist
                //  if found
                //      no: determine the current max tracknumber, increment++
                //      yes: throw an error (stop processing trx) BUSINESS RULE
                //create an instance of the PlaylistTrack 
                //  load with data
                //  stage the instance for adding
                //commit the work via entityframework (ADO.net) to the database

                int tracknumber = 0;
                PlaylistTrack newtrack = null;
                List<string> errors = new List<string>(); //for use by BusinessRuleException
                Playlist exists = (from x in context.Playlists
                                   where x.Name.Equals(playlistname)
                                    && x.UserName.Equals(username)
                                   select x).FirstOrDefault();
                if(exists == null)
                {
                    //exists = new Playlist();
                    //exists.Name = playlistname;
                    //exists.UserName = username;
                   
                    exists = new Playlist()
                    {
                        //pkey is an identity int key
                        Name=playlistname,
                        UserName=username
                    };
                    context.Playlists.Add(exists);
                    tracknumber = 1;
                }
                else
                {
                    //exists has the record instance of the playlist
                    //does the track already exist on the playlist
                    newtrack = (from x in context.PlaylistTracks
                                where x.Playlist.Name.Equals(playlistname)
                                     && x.Playlist.UserName.Equals(username)
                                     && x.TrackId == trackid
                                select x).FirstOrDefault();
                    if (newtrack == null)
                    {
                        //track not on playlist
                        tracknumber = (from x in context.PlaylistTracks
                                       where x.Playlist.Name.Equals(playlistname)
                                            && x.Playlist.UserName.Equals(username)
                                       select x.TrackNumber).Max();
                        tracknumber++;
                    }
                    else
                    {
                        //track already on  playlist
                        //business rule states duplicate tracks on playlist not allowed
                        //violates the busines rule

                        //throw an exception
                        //throw new Exception("Track already on the playlist. Duplicates not allowed");

                        //use the BusinessRuleException class to throw the error
                        //this class takes in a List<string> representing all errors to the handled
                        errors.Add("Track already on the playlist. Duplicates are not allowed");
                    }
                }
                //create/load/add a PlaylistTrack
                newtrack = new PlaylistTrack();

                //load of instance data
                    
                newtrack.TrackId = trackid;
                newtrack.TrackNumber = tracknumber;

                //scenario 1) New playlist
                //   the exists instance is a NEW instance that is YET
                //       to be placed on the sql database
                //   THEREFORE it DOES NOT YET have a primary key value!!!!!!!
                //   the current value of the PlaylistId on the exists instance
                //      is the default system value for an integer (0)
                //scenario 2) Existing playlist
                //   the exists instance has the PlaylistId value

                //the solution to both these scenarios is to use
                //  navigational properties during the ACTUAL .Add command
                //the entityframework will on your behave ensure that the
                //  adding of records to the database will be done in the
                //  appropriate order AND add the missing compound primary key 
                //  value (PlaylistId) to the child record newtrack;
                exists.PlaylistTracks.Add(newtrack);

                //handle the creation of the PlaylistTrack record
                //all validation has been passed????
                if (errors.Count > 0)
                {
                    //no, at least one error was found
                    throw new BusinessRuleException("Adding a Track", errors);
                }
                else
                {
                    //good to go
                    //COMMITTING all staged work
                    context.SaveChanges();
                }
             } //this ensures that the sql connection closes properly
        }//eom
        public void MoveTrack(string username, string playlistname, int trackid, int tracknumber, string direction)
        {
            using (var context = new ChinookSystemContext())
            {
                //trx
                //check to see if the playlist exists
                //  no: error exception
                //  yes:
                //      check to see if song exists
                //      no: error exception
                //      yes:
                //          //up
                //          //check to see if song is at the top
                //          //  yes: error exception
                //          //   no:
                //          //      find record above (tracknumber -1)
                //          //      above record tracknumber modified to tracknumber + 1
                //          //      selected record tracknumber modified to tracknumber -1
                //          //down
                //          //check to see if song is at the bottom
                //          //  yes: error exception
                //          //   no:
                //          //      find record below (tracknumber +1)
                //          //      below record tracknumber modified to tracknumber - 1
                //          //      selected record tracknumber modified to tracknumber +1
                //stage records update
                //commit
               

                List<string> errors = new List<string>(); //for use by BusinessRuleException
                PlaylistTrack moveTrack = null;
                PlaylistTrack otherTrack = null;
                Playlist exists = (from x in context.Playlists
                                   where x.Name.Equals(playlistname)
                                    && x.UserName.Equals(username)
                                   select x).FirstOrDefault();
                if (exists == null)
                {
                    errors.Add("Play list does not exist.");
                }
                else
                {
                    moveTrack = (from x in context.PlaylistTracks
                                 where x.Playlist.Name.Equals(playlistname)
                                  && x.Playlist.UserName.Equals(username)
                                  && x.TrackId == trackid
                                 select x).FirstOrDefault();
                    if (moveTrack == null)
                    {
                        errors.Add("Play list track does not exist.");
                    }
                    else
                    {
                        if (direction.Equals("up"))
                        {
                            //this means the tracknumber of the selected track
                            //    will decrease (track 4 => 3)

                            //prep for move, check if the track is at the top of the list
                            if (moveTrack.TrackNumber == 1)
                            {
                                errors.Add("Song on play list already at the top.");
                            }
                            else
                            {
                                //manipulation of the actual records
                                //the following test conditions identify the PlaylistId value
                                //      && x.Playlist.Name.Equals(playlistname)
                                //      && x.Playlist.UserName.Equals(username)
                                otherTrack = (from x in context.PlaylistTracks
                                              where x.TrackNumber == (moveTrack.TrackNumber - 1)
                                               && x.Playlist.Name.Equals(playlistname)
                                               && x.Playlist.UserName.Equals(username)
                                              select x).FirstOrDefault();
                                if (otherTrack == null)
                                {
                                    errors.Add("Missing required other song track record.");
                                }
                                else
                                {
                                    moveTrack.TrackNumber -= 1;
                                    otherTrack.TrackNumber += 1;
                                }
                            }
                        }
                        else
                        {
                            //down
                            if (moveTrack.TrackNumber == exists.PlaylistTracks.Count)
                            {
                                errors.Add("Song on play list already at the bottom.");
                            }
                            else
                            {
                                //manipulation of the actual records
                                // 4 => 5
                                otherTrack = (from x in context.PlaylistTracks
                                              where x.TrackNumber == (moveTrack.TrackNumber + 1)
                                               && x.Playlist.Name.Equals(playlistname)
                                               && x.Playlist.UserName.Equals(username)
                                              select x).FirstOrDefault();
                                if (otherTrack == null)
                                {
                                    errors.Add("Missing required other song track record.");
                                }
                                else
                                {
                                    moveTrack.TrackNumber += 1;
                                    otherTrack.TrackNumber -= 1;
                                }
                            }
                        }
                    }
                }
                if (errors.Count > 0)
                {
                    throw new BusinessRuleException("Move Track", errors);
                }
                else
                {
                    //stage
                    //a)you can stage an update to alter the entire entity (CRUD)
                    //b)you can stage an update to an entity referencing JUST the property to
                    //      be modified
                    //in this example (b) will be used
                    context.Entry(moveTrack).Property("TrackNumber").IsModified = true;
                    context.Entry(otherTrack).Property(nameof(PlaylistTrack.TrackNumber)).IsModified = true;
                    //commit
                    context.SaveChanges();
                }
            }
        }//eom


        public void DeleteTracks(string username, string playlistname, List<int> trackstodelete)
        {
            using (var context = new ChinookSystemContext())
            {
                //trx
                //check to see if playlist exists
                //   no: error msg
                //   yes: 
                //       create a list of tracks to kept
                //       remove the tracks in the incoming list
                //       re-sequence the kept tracks
                //       commit
                List<string> errors = new List<string>(); //for use by BusinessRuleException
                Playlist exists = (from x in context.Playlists
                                   where x.Name.Equals(playlistname)
                                    && x.UserName.Equals(username)
                                   select x).FirstOrDefault();
                if (exists == null)
                {
                    errors.Add("Play list does not exist.");
                }
                else
                {
                    //find the songs to keep
                    var trackskept = context.PlaylistTracks
                                     .Where(tr => tr.Playlist.Name.Equals(playlistname)
                                        && tr.Playlist.UserName.Equals(username)
                                        && !trackstodelete.Any(tod => tod == tr.TrackId))
                                     .OrderBy(tr => tr.TrackNumber)
                                     .Select(tr => tr);
                    //remove the tracks to delete
                    PlaylistTrack item = null;
                    foreach(int deletetrackid in trackstodelete)
                    {
                        item = context.PlaylistTracks
                                .Where(tr => tr.Playlist.Name.Equals(playlistname)
                                        && tr.Playlist.UserName.Equals(username)
                                        && tr.TrackId == deletetrackid)
                                .Select(tr => tr).FirstOrDefault();
                        if (item != null)
                        {
                            //stage the delete
                            exists.PlaylistTracks.Remove(item);
                        }
                    }

                    //re-sequence
                    int number = 1;
                    foreach(var track in trackskept)
                    {
                        track.TrackNumber = number;
                        context.Entry(track).Property(nameof(PlaylistTrack.TrackNumber)).IsModified = true;
                        number++;
                    }
                    if(errors.Count > 0)
                    {
                        throw new BusinessRuleException("Track removal", errors);
                    }
                    else
                    {
                        //commit
                        context.SaveChanges();
                    }
                    
                }
            }
        }//eom
    }
}
