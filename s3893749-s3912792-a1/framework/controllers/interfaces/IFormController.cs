using s3893749_s3912792_a1.framework.core;

namespace s3893749_s3912792_a1.framework.controllers.interfaces;

public interface IFormController
{

    void OnSuccess(Event @event);
    void OnError(Event @event);

    bool OnSubmit(Event @event);

}