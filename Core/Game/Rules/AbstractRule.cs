using System;
using Core.Interfaces;
using Core.Interfaces.Game;

namespace Core.Game.Rules
{
	public abstract class AbstractRule<TRuleset, TTarget>
		where TRuleset : IGameRule
		where TTarget : IGameObject
	{
	private Func<TTarget, bool> Rule { get; set; }

	public AbstractRule(Func<TTarget, bool> rule)
	{
		Rule = rule;
	}

	public bool ApplyRule(TTarget gameObject) => Rule(gameObject);

	}
}