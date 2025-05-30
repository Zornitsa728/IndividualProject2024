﻿namespace RecipeApp.Common
{
    public static class EntityValidationConstants
    {
        public static class Recipe
        {
            public const int TitleMinLength = 1;

            public const int TitleMaxLength = 100;

            public const int InstructionsMinLength = 10;

            public const int InstructionsMaxLength = 6000;

            public const int DescriptionMinLength = 20;

            public const int DescriptionMaxLength = 2000;

            public const string CreatedOnDateFormat = "dd/MM/yyyy";

            public const int ImageUrlMaxLength = 2100;
        }

        public static class Ingredient
        {
            public const int NameMinLength = 1;

            public const int NameMaxLength = 100;

            public const double QuantityMinLength = 0.1;

            public const int QuantityMaxLength = 1000;
        }

        public static class Category
        {
            public const int NameMinLength = 1;
            public const int NameMaxLength = 25;
        }

        public static class Comment
        {
            public const int ContentMinLength = 1;
            public const int ContentMaxLength = 250; // TODO: check what is normal length for comments
            public const string datePostedFormat = "dd/MM/yyyy";
        }

        public static class Rating
        {
            public const int ScoreMinValue = 1;
            public const int ScoreMaxValue = 5;
        }

        public static class User
        {
            public const int NameMinLength = 1;
            public const int NameMaxLength = 747;
        }

        public static class Cookbook
        {
            public const int TitleMinLength = 1;

            public const int TitleMaxLength = 100;

            public const int DescriptionMinLength = 5;

            public const int DescriptionMaxLength = 500;
        }
    }
}
