public interface IInteractable
{
    void Interact();
}

public interface IInteractionPrompt
{
    string Prompt { get; }
}
