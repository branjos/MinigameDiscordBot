using MinigamesDiscordBot.Util;
using System;
using System.Collections.Generic;

namespace MinigamesDiscordBot.Entities
{
    class CastleWarsGame
    {
        //holds the names of users on either team
        List<CwsUser> saradominTeam;
        List<CwsUser> zamorakTeam;

        //holds the name of the user that started the game and if the game is going
        public string GameController { get; set; }
        public DateTime startTime { get; set; }
        public bool GameGoing { get; set; }

        public CastleWarsGame()
        {
            saradominTeam = new List<CwsUser>();
            zamorakTeam = new List<CwsUser>();
        }

        //adds the user as a perm to a team
        public string AddPerm(string user, string side)
        {
            CwsUser u = new CwsUser(user);
            CwsUser u2 = new CwsUser(user + "*");
            if (!saradominTeam.Contains(u) && !zamorakTeam.Contains(u) && !saradominTeam.Contains(u2) && 
                !zamorakTeam.Contains(u2))
            {
                if (side == "sara")
                {
                    saradominTeam.Add(u2);
                    return u.Username + " has been added as a perm to the Saradomin team.";
                }
                else if (side == "zam")
                {
                    zamorakTeam.Add(u2);
                    return u.Username + " has been added as a perm to the Zamorak team.";
                }
                else
                {
                    return "Invalid side";
                }
            }
            else
            {
                return "That user is already on a team!";
            }
        }

        //adds the user to the team with the least amt of people, or to sara if equal
        public string AddRotating(string user)
        {
            CwsUser u = new CwsUser(user);
            CwsUser u2 = new CwsUser(user + "*");
            if (!saradominTeam.Contains(u) && !zamorakTeam.Contains(u) && !saradominTeam.Contains(u2) &&
                !zamorakTeam.Contains(u2))
            {
                if(saradominTeam.Count == zamorakTeam.Count)
                {
                    saradominTeam.Add(u);
                    return u.Username + " has been added to the Saradomin team.";
                }
                else
                {
                    if(saradominTeam.Count < zamorakTeam.Count)
                    {
                        saradominTeam.Add(u);
                        return u.Username + " has been added to the Saradomin team.";
                    }
                    else
                    {
                        zamorakTeam.Add(u);
                        return u.Username + " has been added to the Zamorak team.";
                    }
                }
            }
            else
            {
                return "That user is already on a team!";
            }
        }

        //removes the user from either of the groups
        public string RemoveUser(string user)
        {
            CwsUser u = new CwsUser(user);
            CwsUser u2 = new CwsUser(user + "*");

            if (saradominTeam.Contains(u))
            {
                saradominTeam.Remove(u);
                return user + " was removed from the Saradomin team.";
            }             
            else if (zamorakTeam.Contains(u))
            {
                zamorakTeam.Remove(u);
                return user + " was removed from the Zamorak team.";
            }
            else if (saradominTeam.Contains(u2))
            {
                saradominTeam.Remove(u2);
                return user + " was removed from the Saradomin team.";
            }
            else if (zamorakTeam.Contains(u2))
            {
                zamorakTeam.Remove(u2);
                return user + " was removed from the Zamorak team.";
            }
            else
            {
                return user + " was not on any teams!";
            }           
        }

        //clears all from game (games closed)
        public string ClearGame()
        {
            saradominTeam.Clear();
            zamorakTeam.Clear();
            return "Game Closed.";
        }

        //starts a new round and switches teams up
        public string NewRound()
        {
            //temporary lists to hold the names of users being switched
            List<CwsUser> temp1 = new List<CwsUser>();
            List<CwsUser> temp2 = new List<CwsUser>();

            //loop through all sara team members, if not a perm - put them in a temp list
            for(int i = 0; i < saradominTeam.Count; ++i)
            {
                saradominTeam[i].ConsecutiveWins++;

                if (!saradominTeam[i].Username.Contains("*"))
                {
                    temp1.Add(saradominTeam[i]);
                    saradominTeam.RemoveAt(i);
                }
            }
            
            //loop through all zam team members, if not a perm - put them in a temp list
            for (int i = 0; i < zamorakTeam.Count; ++i)
            {
                if (!zamorakTeam[i].Username.Contains("*"))
                {
                    temp2.Add(zamorakTeam[i]);
                    zamorakTeam.RemoveAt(i);
                }
            }

            //add the non-perm members to the new team
            for (int i = 0; i < temp1.Count; ++i)
            {
                zamorakTeam.Add(temp1[i]);
            }
            for (int i = 0; i < temp2.Count; ++i)
            {
                saradominTeam.Add(temp2[i]);
            }

            EvenOutSides(); //to ensure sides are even

            return "New teams assigned.";
        }

        private void EvenOutSides()
        {
            //we want sara to be the heavier team, so if sara team has 2 more members than zam, we need to move one
            if (saradominTeam.Count > zamorakTeam.Count + 1) 
            {
                int numberToMove = saradominTeam.Count - zamorakTeam.Count + 1;

                //move the users with the most wins until the numbertomove is 0;
                while (numberToMove > 0)
                {
                    int mostWins = 0; 
                
                    //check to see the most consecutive wins
                    foreach(CwsUser u in saradominTeam)
                    {
                        //if user has more wins than the last person and is not a perm member
                        if (u.ConsecutiveWins > mostWins && !CheckIfPerm(u)) 
                        {
                            mostWins = u.ConsecutiveWins;
                        }
                    }

                    //loop thorugh all users finding a user who matches the most consecutive wins
                    for(int i = 0; i < saradominTeam.Count; ++i)
                    {
                        //if it meets mostWins
                        if(saradominTeam[i].ConsecutiveWins == mostWins && !CheckIfPerm(saradominTeam[i]))
                        {
                            zamorakTeam.Add(saradominTeam[i]); //add user to zam team
                            saradominTeam.RemoveAt(i); //remove user from sara team
                            numberToMove--; //subtract 1 from number to remove
                            break; //exit the loop
                        }
                    }
                  
                }

            }
            //if zamorak team has more members than sara
            else if (saradominTeam.Count < zamorakTeam.Count)
            {
                int numberToMove = zamorakTeam.Count - saradominTeam.Count;

                //move the users with the most wins until the numbertomove is 0;
                while (numberToMove > 0)
                {
                    int leastWins = 0;

                    //check to see the most consecutive wins
                    foreach (CwsUser u in zamorakTeam)
                    {
                        if (u.ConsecutiveWins < leastWins && !CheckIfPerm(u))
                        {
                            leastWins = u.ConsecutiveWins;
                        }
                    }

                    //loop thorugh all users finding a user who matches the least consecutive wins
                    for (int i = 0; i < zamorakTeam.Count; ++i)
                    {
                        //if it meets mostWins
                        if (zamorakTeam[i].ConsecutiveWins == leastWins && !CheckIfPerm(zamorakTeam[i]))
                        {
                            saradominTeam.Add(saradominTeam[i]); //add user to sara team
                            zamorakTeam.RemoveAt(i); //remove user from zam team
                            numberToMove--; //subtract 1 from number to remove
                            break; //exit the loop
                        }
                    }
                }

            }
        }

        public string OutputTable()
        {

            if (GameGoing)
            {
                string header = "```\n";
                header += "Coordinator: " + GameController + "\n";
                header += "Start Time: " + startTime + "\n";
                header += "```";

                CastleWarsOutputTable o = new CastleWarsOutputTable();

                if (saradominTeam.Count >= zamorakTeam.Count) //if sara team is equal to or greater than zam
                {
                    for (int i = 0; i < saradominTeam.Count; ++i) //loop thorugh all sara team
                    {
                        if (i + 1 > zamorakTeam.Count) //if iterator is greater than number of zam players
                        {
                            o.addLine(saradominTeam[i].Username, ""); //add line with nothing on one side
                        }
                        else
                        {
                            o.addLine(saradominTeam[i].Username, zamorakTeam[i].Username); //add both names
                        }
                    }

                    o.insertEnd();

                    return header + o.output;
                }
                else //this is basically the above in reverse
                {
                    for (int i = 0; i < zamorakTeam.Count; ++i)
                    {
                        if (i + 1 > saradominTeam.Count)
                        {
                            o.addLine("", zamorakTeam[i].Username);
                        }
                        else
                        {
                            o.addLine(saradominTeam[i].Username, zamorakTeam[i].Username);
                        }
                    }

                    o.insertEnd();

                    return o.output;
                }
            }
            else
            {
                return "```\n No games are currently running.\n```";
            }
        }

        private bool CheckIfPerm(CwsUser u)
        {
            if (u.Username.Contains("*"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
