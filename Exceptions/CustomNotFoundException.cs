namespace stackup_quiz_api.Exceptions;

public class CustomNotFoundException(string errorMessage):Exception(errorMessage){ };