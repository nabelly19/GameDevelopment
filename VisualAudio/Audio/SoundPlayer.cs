using System.Media;

public class SoundPlayer
{
    private System.Media.SoundPlayer song;

    public SoundPlayer()
    {
        this.song = new System.Media.SoundPlayer();
        this.song.SoundLocation = "assets/songs/Haunt.wav"; // DEBUG MODE
    }


    public void ChangeSoundLocation(System.Media.SoundPlayer sender)
    {
        this.song.Stop();
        this.song.Dispose();
        this.song = sender;
        // this.song.SoundLocation = "...assets/songs/OMORI OST - 106 GOLDENVENGEANCE.wav";
        this.song.Load();
        this.song.PlayLooping();
    }
    public void Play() => this.song.PlayLooping();
}
