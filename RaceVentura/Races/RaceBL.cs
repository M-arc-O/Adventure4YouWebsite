﻿using RaceVentura.Models;
using RaceVenturaData;
using RaceVenturaData.Models;
using RaceVenturaData.Models.Races;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RaceVentura.Races
{
    public class RaceBL : RaceBaseBL, IGenericCrudBL<Race>
    {
        public RaceBL(IRaceVenturaUnitOfWork unitOfWork, ILogger<RaceBL> logger) : base(unitOfWork, logger)
        {

        }

        public IEnumerable<Race> Get(Guid userId)
        {
            var links = _UnitOfWork.UserLinkRepository.Get(link => link.UserId == userId);
            return _UnitOfWork.RaceRepository.Get(null, r => r.OrderBy(race => race.Name)).Where(r => links.Any(link => link.RaceId == r.RaceId));
        }

        public Race GetById(Guid userId, Guid raceId)
        {
            CheckUserIsAuthorizedForRace(userId, raceId, RaceAccessLevel.Read);

            var race = _UnitOfWork.RaceRepository.Get(r => r.RaceId == raceId, null,
                "Teams,Teams.VisitedPoints,Teams.FinishedStages," +
                "Stages,Stages.Points").FirstOrDefault();
            if (race == null)
            {
                throw new BusinessException($"No race with ID {raceId} found.", BLErrorCodes.NotFound);
            }

            race.Teams = race.Teams.OrderBy(team => team.Number).ToList();
            race.Stages = race.Stages.OrderBy(stage => stage.Number).ToList();
            race.Stages.ForEach(stage => stage.Points = stage.Points?.OrderBy(point => point.Name).ToList());

            return race;
        }

        public void Add(Guid userId, Race race)
        {
            CheckIfRaceNameExists(race.Name);

            _UnitOfWork.RaceRepository.Insert(race);
            _UnitOfWork.Save();

            _UnitOfWork.UserLinkRepository.Insert(new UserLink
            {
                RaceId = race.RaceId,
                UserId = userId,
                RaceAccess = RaceAccessLevel.Owner
            });

            _UnitOfWork.Save();
        }

        public void Edit(Guid userId, Race newEntity)
        {
            CheckIfRaceExsists(userId, newEntity.RaceId);
            CheckUserIsAuthorizedForRace(userId, newEntity.RaceId, RaceAccessLevel.ReadWrite);

            var race = _UnitOfWork.RaceRepository.GetByID(newEntity.RaceId);

            if (!race.Name.ToUpper().Equals(newEntity.Name.ToUpper()))
            {
                CheckIfRaceNameExists(newEntity.Name);
            }

            race.Name = newEntity.Name;
            race.CoordinatesCheckEnabled = newEntity.CoordinatesCheckEnabled;
            race.AllowedCoordinatesDeviation = newEntity.AllowedCoordinatesDeviation;
            race.PenaltyPerMinuteLate = newEntity.PenaltyPerMinuteLate;
            race.SpecialTasksAreStage = newEntity.SpecialTasksAreStage;
            race.MaximumTeamSize = newEntity.MaximumTeamSize;
            race.MinimumPointsToCompleteStage = newEntity.MinimumPointsToCompleteStage;
            race.StartTime = newEntity.StartTime;
            race.MaxDuration = newEntity.MaxDuration;

            _UnitOfWork.RaceRepository.Update(race);

            _UnitOfWork.Save();
        }

        public void Delete(Guid userId, Guid raceId)
        {
            CheckIfRaceExsists(userId, raceId);
            UserLink userLink = CheckUserIsAuthorizedForRace(userId, raceId, RaceAccessLevel.Owner);

            _UnitOfWork.UserLinkRepository.Delete(userLink);
            _UnitOfWork.RaceRepository.Delete(raceId);

            _UnitOfWork.Save();
        }

        private void CheckIfRaceNameExists(string name)
        {
            if (_UnitOfWork.RaceRepository.Get().Any(race => race.Name.ToUpper().Equals(name.ToUpper())))
            {
                throw new BusinessException($"A race with name '{name}' already exists.", BLErrorCodes.Duplicate);
            }
        }
    }
}
