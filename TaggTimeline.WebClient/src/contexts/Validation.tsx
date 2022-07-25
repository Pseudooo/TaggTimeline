import {
  createContext,
  FunctionComponent,
  PropsWithChildren,
  useMemo,
} from "react";
import { ValidationResponse } from "../validation/rules";

export interface ValidationContext {
  validationResponses: ValidationResponse[];
}

const ValidationContext = createContext<ValidationContext>({
  validationResponses: [],
});

/**
 * Creates a validation context provider, which can handle registering validation dependencies and
 * general state management for control validity
 *
 * @param props Component props
 */
export const ValidationProvider: FunctionComponent<PropsWithChildren> = ({
  children,
}) => {
  const validationResponses: ValidationResponse[] = [];

  // @TODO: Provide methods to inject validation responses uniquely

  // Use Memo'd versions to prevent re-rendering unnecessarily
  const memoedValue = useMemo<ValidationContext>(
    () => ({
      validationResponses,
    }),
    [validationResponses]
  );

  return (
    <ValidationContext.Provider value={memoedValue}>
      {children}
    </ValidationContext.Provider>
  );
};
