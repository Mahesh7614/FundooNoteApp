﻿

namespace FundooModel
{
    public class NoteModel
    {
        public int NoteID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Reminder { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public bool Archive { get; set; }
        public bool PinNotes { get; set; }
        public bool Trash { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public int UserID { get; set; }

    }
}
