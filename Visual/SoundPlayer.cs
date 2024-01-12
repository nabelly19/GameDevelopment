public class SoundPlayer
{
    private System.Media.SoundPlayer song;

    public SoundPlayer()
    {
        this.song = new System.Media.SoundPlayer();
        // song.SoundLocation = "../../../Midia/songs/OMORI OST - 106 GOLDENVENGEANCE.wav"; // DEBUG MODE
        song.SoundLocation = "./Midia/songs/OMORI OST - 106 GOLDENVENGEANCE.wav";
    }

    public void Play() => this.song.PlayLooping();
}
