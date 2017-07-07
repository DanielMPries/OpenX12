using openx12.Attributes;

/// <summary>
/// Acknowledgment Requested
/// Code indicating sender’s request for an interchange acknowledgment
/// </summary>
public enum AcknowledgementRequestedType
{
    /// <summary>
    /// No Interchange Acknowledgment Requested
    /// </summary>
    [Code("0", "No Interchange Acknowledgment Requested")]
    NoInterchangeAcknowledgmentRequested,

    /// <summary>
    /// Interchange Acknowledgment Requested (TA1)
    /// </summary>
    [Code("1", "Interchange Acknowledgment Requested")]
    InterchangeAcknowledgmentRequested
}