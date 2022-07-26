export type ValidationResponse = string[];

/**
 * Validates whether or not a provided value meets a "required" critera
 * @param value The value to check if it exists
 * @returns Relevant required errors
 */
// eslint-disable-next-line
export function required(value: any): ValidationResponse {
  const errored: ValidationResponse = ["Value is required"];

  if (value === undefined || value === null) {
    return errored;
  }

  if (typeof value === "string" && value.length === 0) {
    return errored;
  }

  return [];
}

/**
 * Validates a password to be valid for the backend
 * @todo Potentially consider support for non-english characters
 * @param password The password to validate
 * @returns All relevant password errors
 */
export function validatePassword(password: string): ValidationResponse {
  const errors: string[] = [];
  const specailCharacters = /[ `!@#$%^&*()_+\-=[\]{};':"\\|,.<>/?~]/;

  if (password.length < 6) {
    errors.push("Passwords must be at least 6 characters");
  }

  // Magical regex
  if (/^(.)\1+$/.test(password)) {
    errors.push("Passwords must use at least 1 different characters");
  }

  if (!specailCharacters.test(password)) {
    errors.push("Passwords must contain at least one special character");
  }

  if (!/\d/.test(password)) {
    errors.push("Passwords must contain at least one number");
  }

  if (!/[a-z]/.test(password)) {
    errors.push("Passwords must container at least one lower case letter");
  }

  if (!/[A-Z]/.test(password)) {
    errors.push("Passwords must container at least one lower case letter");
  }

  return errors;
}
