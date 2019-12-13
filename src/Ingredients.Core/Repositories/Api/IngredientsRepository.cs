using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Ingredients.Core.Models;
using Ingredients.Core.Models.Results;
using Ingredients.Core.Repositories.Base;
using MongoDB.Bson;

namespace Ingredients.Core.Repositories.Api
{
	/// <summary>
	/// Repository for <see cref="Ingredient"/> objects.
	/// </summary>
	public class IngredientsRepository : IRepository<Ingredient, ObjectId>
	{
		private readonly IRepository<Ingredient, ObjectId> _backingRepository;

		/// <summary>
		/// Create a new <see cref="IngredientsRepository"/> with the specified backing repository implementation.
		/// </summary>
		/// <param name="backingRepository"></param>
		public IngredientsRepository(IRepository<Ingredient, ObjectId> backingRepository)
		{
			_backingRepository = backingRepository;
		}

		/// <inheritdoc />
		public Ingredient Get(ObjectId id)
		{
			return _backingRepository.Get(id);
		}

		/// <inheritdoc />
		public IEnumerable<Ingredient> Get(Expression<Func<Ingredient, bool>> filterExpression)
		{
			return _backingRepository.Get(filterExpression);
		}

		/// <inheritdoc />
		public IOperationResult Create(Ingredient data)
		{
			return _backingRepository.Create(data);
		}
	}
}