using MyBank.framework.core;

namespace MyBank.framework.controllers.interfaces;

public interface IFormController
{

    void OnSuccess(Event @event);
    void OnError(Event @event);

    bool OnSubmit(Event @event);

}