namespace RedeyeMusic.Common
{
    public static class EntityValidationConstants
    {
        public static class Genre
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
        }
        public static class Song
        {
            public const int TitleMinLength = 2;
            public const int TitleMaxLength = 50;

            public const int DurationMinLengthInSeconds = 90;
            public const int DurationMaxLengthInSeconds = 600;

            public const int LyricsMinLength = 20;
            public const int LyricsMaxLength = 1000;
        }
        public static class Playlist
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
        }
        public static class Album
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;

            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 200;
        }

        public static class Artist
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
        }
    }
}
