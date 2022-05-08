namespace WebStore.Database.Models.Enums;

public enum PaymentState
{
    Pending,
    Succeeded,
    Canceled,
    WaitingForCapture,
    Refund
}