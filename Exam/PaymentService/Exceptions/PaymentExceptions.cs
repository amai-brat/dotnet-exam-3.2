namespace PaymentService.Exceptions;

public class NotFoundException(string message) : Exception(message);
public class ArgumentValidationException(string message) : Exception(message);