﻿using Adventure4You.DatabaseContext;
using Adventure4You.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Adventure4You
{
    public class RaceBL : BaseBL, IRaceBL
    {
        private readonly IStageBL _StageBL;

        public RaceBL(IAdventure4YouDbContext context, IStageBL stageBL) : base(context)
        {
            _StageBL = stageBL;
        }

        public BLReturnCodes GetAllRaces(Guid userId, out List<Race> races)
        {
            races = null;
            var links = _Context.UserLinks.Where(link => link.UserId == userId);

            races = _Context.Races.Where(race => links.Any(link => link.RaceId == race.Id)).OrderBy(race => race.Name).ToList();
            if (races == null)
            {
                return BLReturnCodes.NoRacesFound;
            }

            return BLReturnCodes.Ok;
        }

        public BLReturnCodes GetRaceDetails(Guid userId, Guid raceId, out Race race)
        {
            race = null;

            if (CheckIfUserHasAccessToRace(userId, raceId) == null)
            {
                return BLReturnCodes.UserUnauthorized;
            }

            race = GetRace(raceId);
            if (race == null)
            {
                return BLReturnCodes.UnknownRace;
            }

            return BLReturnCodes.Ok;
        }

        public BLReturnCodes AddRace(Guid userId, Race race)
        {
            if (!CheckIfRaceNameExists(race.Name))
            {
                _Context.Races.Add(race);
                _Context.SaveChanges();

                _Context.UserLinks.Add(new UserLink
                {
                    RaceId = race.Id,
                    UserId = userId
                });

                _Context.SaveChanges();
            }
            else
            {
                return BLReturnCodes.Duplicate;
            }

            return BLReturnCodes.Ok;
        }

        public BLReturnCodes DeleteRace(Guid userId, Guid raceId)
        {
            var userLink = CheckIfUserHasAccessToRace(userId, raceId);
            if (userLink == null)
            {
                return BLReturnCodes.UserUnauthorized;
            }

            var race = GetRace(raceId);
            if (race != null)
            {
                _StageBL.RemoveStages(userId, raceId);

                _Context.UserLinks.Remove(userLink);
                _Context.Races.Remove(race);
                
                _Context.SaveChanges();
            }
            else
            {
                return BLReturnCodes.UnknownRace;
            }

            return BLReturnCodes.Ok;
        }

        public BLReturnCodes EditRace(Guid userId, Race raceNew)
        {
            if (CheckIfUserHasAccessToRace(userId, raceNew.Id) == null)
            {
                return BLReturnCodes.UserUnauthorized;
            }

            var race = GetRace(raceNew.Id);
            if (race == null)
            {
                return BLReturnCodes.UnknownRace;
            }

            if (race.Name.ToUpper().Equals(raceNew.Name.ToUpper()) || !CheckIfRaceNameExists(raceNew.Name))
            {
                race.Name = raceNew.Name;
                race.CoordinatesCheckEnabled = raceNew.CoordinatesCheckEnabled;
                race.SpecialTasksAreStage = raceNew.SpecialTasksAreStage;
                race.MaximumTeamSize = raceNew.MaximumTeamSize;
                race.MinimumPointsToCompleteStage = raceNew.MinimumPointsToCompleteStage;
                race.StartTime = raceNew.StartTime;
                race.EndTime = raceNew.EndTime;
                _Context.SaveChanges();

                return BLReturnCodes.Ok;
            }
            else
            {
                return BLReturnCodes.Duplicate;
            }
        }

        private Race GetRace(Guid raceId)
        {
            return _Context.Races.FirstOrDefault(race => race.Id == raceId);
        }

        private bool CheckIfRaceNameExists(string name)
        {
            return _Context.Races.Any(race => race.Name.ToUpper().Equals(name.ToUpper()));
        }
    }
}
