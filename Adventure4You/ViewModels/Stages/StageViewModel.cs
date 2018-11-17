﻿using Adventure4You.Models;

namespace Adventure4You.ViewModels.Stages
{
    public class StageViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public StageViewModel()
        {
        }

        public StageViewModel(Stage stage)
        {
            Id = stage.Id;
            Name = stage.Name;
        }
    }
}
