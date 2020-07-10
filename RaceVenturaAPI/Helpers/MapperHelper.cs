﻿using RaceVenturaAPI.ViewModels.Identity.Mappings;
using RaceVenturaAPI.ViewModels.Races.MappingProfiles;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace RaceVenturaAPI.Helpers
{
    public static class MapperHelper
    {

        public static void AddAutoMapper(IServiceCollection services)
        {   // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ViewModelToEntityMappingProfile());
                mc.AddProfile(new PointMappingProfile());
                mc.AddProfile(new StageMappingProfile());
                mc.AddProfile(new RaceMappingProfile());
                mc.AddProfile(new TeamMappingProfile());
                mc.AddProfile(new VisitedPointMappingProfile());
                mc.AddProfile(new FinishedStageMappingProfile());
                mc.AddProfile(new ResultMappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}