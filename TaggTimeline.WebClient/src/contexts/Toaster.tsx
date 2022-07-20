import { Alert, AlertColor, AlertTitle, Collapse, Stack } from "@mui/material";
import {
  createContext,
  FunctionComponent,
  PropsWithChildren,
  ReactNode,
  useContext,
  useEffect,
  useMemo,
  useRef,
  useState,
} from "react";
import { TransitionGroup } from "react-transition-group";

interface ToasterDefinition {
  title?: string | ReactNode;
  message: string | ReactNode;
  severity?: AlertColor;
  lifetime?: number;
}

interface ToasterDefinitionInternal extends ToasterDefinition {
  id: number;
}

interface ToasterProps {
  definition: ToasterDefinitionInternal;
  onClose(id: number): void;
}

/**
 * Craetes a "toaster" notification. Is currently just an alert.
 * @param props The props for this component
 */
const Toaster: FunctionComponent<ToasterProps> = ({ definition, onClose }) => {
  // Create self-closing functionality
  useEffect(() => {
    const lifetime = definition.lifetime ?? 5000;
    if (lifetime > 0) {
      const timer = setTimeout(() => {
        onClose(definition.id);
      }, lifetime);

      return () => {
        clearTimeout(timer);
      };
    }
  }, []);

  return (
    <Alert
      severity={definition.severity ?? "info"}
      onClose={() => {
        onClose(definition.id);
      }}
    >
      {definition.title && <AlertTitle>{definition.title}</AlertTitle>}
      {definition.message}
    </Alert>
  );
};

interface ToasterContextType {
  createToaster(options: ToasterDefinition): void;
}

const ToasterContext = createContext<ToasterContextType>(
  {} as ToasterContextType
);

/**
 * Creates a provider for creating toaster notifications
 * @param props The props for this component
 */
export const ToasterProvider: FunctionComponent<PropsWithChildren> = ({
  children,
}) => {
  const [toasters, setToasters] = useState<ToasterDefinitionInternal[]>([]);
  const currentId = useRef(0);

  /**
   * Creates a toaster
   * @param message The text for the toaster
   */
  function createToaster(options: ToasterDefinition) {
    const internalDef = {
      ...options,
      id: currentId.current,
    };
    currentId.current += 1;
    setToasters((prevState) => [...prevState, internalDef]);
  }

  // Use Memo'd versions to prevent re-rendering unnecessarily
  const memoedValue = useMemo<ToasterContextType>(
    () => ({
      createToaster,
    }),
    []
  );

  /**
   * Removes the toaster with the given ID
   * @param id The id of the toaster to remove
   */
  const handleRemoveToaster = (id: number) => {
    setToasters((prev) => [...prev.filter((i) => i.id !== id)]);
  };

  // Create all the active toasters
  const toasterElems = toasters.map((definition, id) => (
    <Collapse key={definition.id}>
      <Toaster definition={definition} onClose={handleRemoveToaster} />
    </Collapse>
  ));

  return (
    <ToasterContext.Provider value={memoedValue}>
      {children}
      <Stack
        spacing={1}
        padding={1}
        alignItems="baseline"
        sx={{
          position: "fixed",
          bottom: 0,
          left: 0,
          right: 0,
          // pointerEvents: "none",
        }}
      >
        <TransitionGroup component={null}>{toasterElems}</TransitionGroup>
      </Stack>
    </ToasterContext.Provider>
  );
};

/**
 * Helper function for getting the app's toaster context
 */
export function useToaster() {
  return useContext(ToasterContext);
}
