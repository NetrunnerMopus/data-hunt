﻿namespace model.choices.trash
{
    public interface ITrashOption
    {
        void Perform(Game game);
        string Art { get; }
    }
}