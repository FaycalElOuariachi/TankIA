# metaGenBayes Project

## Presentation

metaGenBayes computes specific probabilites (targets) given evidences (hard or soft). Using bayesian networks and Shafer-Shenoy inference, it writes in different languages the code that will later calculates the targets.

## How to Use

metaGenBayes uses a yaml configuration file, which follows these guidelines : a *bayesnet*, a list of *evidence* and *target*, a *language*, the *filename* and the *function* you want for the generated code.
Languages supported so far are python (via numpy), php and Javascript.

metaGenBayes.py is the main file. After specifying your inputs in the config.yaml, metaGenBayes.py will create the generated file in the current repository.

## Components

In the [Compiler] folder you will find the functions that creates an array of instructions that will later be translated in the different languages. In order to have the best results possible, we used barren nodes and the Bayes-Ball algorithm to simplify the given bayesian network.

The [Generator] folder lists the different python files used to generate the code from the compiled array of instructions.
