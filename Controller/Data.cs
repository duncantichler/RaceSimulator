﻿using Model;
using System;
using static Model.Track;
using System.Drawing;

namespace Controller
{
    public static class Data
    {
        public static Competition Competition { get; set; }
        public static Race CurrentRace { get; set; }

        public static event EventHandler<NextRaceEventArgs> NextRaceEvent;

        public static void Initialize()
        {
            Competition = new Competition();
            AddParticipants();
            AddTracks();
        }

        public static void AddParticipants()
        {
            Competition.Participants.Add(new Driver("Driver1", new Car(19, 10, 20, false), IParticipant.TeamColors.Red));
            Competition.Participants.Add(new Driver("Driver2", new Car(18, 10, 19, false), IParticipant.TeamColors.Green));
            Competition.Participants.Add(new Driver("Driver3",new Car(16, 10, 17, false), IParticipant.TeamColors.Blue));
            Competition.Participants.Add(new Driver("Driver4",new Car(20, 10, 14, false), IParticipant.TeamColors.Yellow));
            Competition.Participants.Add(new Driver("Driver5",new Car(20, 10, 14, false), IParticipant.TeamColors.Yellow));
        }

        public static void AddTracks()
        {
            Competition.Tracks.Enqueue(new Track("rondje", new[]
            {           
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.Finish,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
            }));

            Competition.Tracks.Enqueue(new Track("lange ovaal", new[]
            {
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.Finish,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
            }));
            Competition.Tracks.Enqueue(new Track("lange ovaal 2", new[]
            {
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.Finish,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
            }));
            
                foreach (var t in Competition.Tracks)
                { 
                    Competition.Tracklist.Add(t.Name);
                }
        }

        public static void NextRace()
        {
            CurrentRace?.CleanUp();
            Competition.TimesBrokenDown.Clear();
            CurrentRace?.LapsCompleted.Clear();
            Competition.Speed.Clear();
            Track currentTrack = Competition.NextTrack();
            if (currentTrack != null)
            {
                CurrentRace = new Race(currentTrack, Competition.Participants);
                NextRaceEvent?.Invoke(null, new NextRaceEventArgs() { Race = CurrentRace });
                CurrentRace.Start();
            }
        }
    }
}
