﻿using Adventure4You.DatabaseContext;
using Adventure4You.Models;
using Adventure4You.Models.Points;
using Adventure4You.Models.Stages;
using Adventure4You.Models.Teams;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Adventure4You
{
    public abstract class BaseBL
    {
        protected readonly IAdventure4YouDbContext _Context;

        public BaseBL(IAdventure4YouDbContext context)
        {
            _Context = context;
        }

        protected List<Race> GetRaces()
        {
            return _Context.Races
                .Include(r => r.Stages)
                .ThenInclude(s => s.Points)
                .Include(r => r.Teams)
                .ThenInclude(t => t.PointsVisited)
                .Include(r => r.Teams)
                .ThenInclude(t => t.StagesFinished)
                .Include(r => r.Teams)
                .ThenInclude(t => t.RaceFinished)
                .Include(r => r.Teams)
                .ThenInclude(t => t.RegisteredPhoneIds).ToList();
        }

        protected Race GetRaceById(Guid raceId)
        {
            return GetRaces().FirstOrDefault(r => r.RaceId == raceId);
        }

        protected Race GetRaceByStageId(Guid stageId)
        {
            return GetRaces().FirstOrDefault(r => r.Stages.Any(s => s.StageId == stageId));
        }

        protected Race GetRaceByTeamId(Guid teamId)
        {
            return GetRaces().FirstOrDefault(r => r.Teams.Any(t => t.TeamId == teamId));
        }

        protected Stage GetStageById(Guid stageId)
        {
            return GetRaceByStageId(stageId).Stages.FirstOrDefault(s => s.StageId == stageId);
        }

        protected Team GetTeamById(Guid teamId)
        {
            return GetRaceByTeamId(teamId).Teams.FirstOrDefault(t => t.TeamId == teamId);
        }

        protected UserLink CheckIfUserHasAccessToRace(Guid userId, Guid raceId)
        {
            return _Context.UserLinks.FirstOrDefault(link => link.UserId == userId && link.RaceId == raceId);
        }
    }
}
