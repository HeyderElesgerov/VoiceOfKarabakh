namespace VoiceOfKarabakh.Domain.Factory.Tag
{
    public class TagFactory
    {
        public Models.Tag GetTag()
        {
            return new Models.Tag();
        }

        public Models.Tag GetTag(Models.LocalizationSet localizationSet)
        {
            var tag = GetTag();
            tag.TitleLocalizationSet = localizationSet;
            return tag;
        }
    }
}
